using LEX_SubscriptionService.AsyncDataServices;
using LEX_SubscriptionService.Data;
using LEX_SubscriptionService.Extensions;
using LEX_SubscriptionService.Helpers;
using LEX_SubscriptionService.Helpers.ExceptionMiddleware;
using LEX_SubscriptionService.SyncDataServices.Grpc;
using LEX_SubscriptionService.SyncDataServices.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/*  ASPEKT/ODGOVORNOST ovog servisa je zaprimanje pretplatničkih zahtjeva:
    - otvoriti metodu za autentifikaciju (prima username i lozinku a vraća token) - token traje 15 minuta
    - zaprimiti pretplatnički zahtjev
        - zahtjev mora biti autentificiran tokenom (Bearer)
    - obraditi zahtjev
        - podaci o pretplatniku - Entity
        - podaci o pretplati - Subscription    
    - identificirati tko je uputio zahtjev (preko SourceKey) - na što je pretplata - Service
        - drugi servis, vanjska ili interna aplikacija, mail server - SourceKey se nalazi u Headeru requesta
            - kada nema SourceKey-a 
                - nema pristupa
            - kada  SourceKey-a nije dobar
                - nema pristupa
    - poslati odgovor
        - zahtjev zaprimljen - tekstualni odgovor koji zahvaća pravno tumačenje aspekta zahtjeva
        - iz LegalSettings servisa uzeti (gRPC) podatke o člancima 5, 6 i 7 - ulazi u odgovor na poslanu pretplatu
            - primljeni zahtjev - ako NIJE servis dostupan   
            - primljeni zahtjev  + citirati članke (iz LegalSettings servisa) - ako JE servis dostupan 
    - poslati asinkronu poruku da je pretplatnički zahtjev zaprimljen (MQ)
    - handlati grešku u komunikaciji sa servisima (gRPC i MQ) kada servisi nisu dostupni
        - gRPC - u responsu - Servis nastavi raditi
        - MQ - u responsu je normalna odgovor ali u ispisu na servisu je poruka - Servis nastavi raditi
*/
/* 
    FUNKCIONALNOSTI SERVISA
    - API
        - metode za kreiranje i čitanje
        - Authorize i AllowAnonymous
        - Mapiranje (AutoMapper)
    - MQ 
        - šalje
    - gRPC
        - prima
        - šalje
    - DTO
        - read, published, create
    - Middleware
    - DB
        - Json file - simulira bazne podatke
        - EntityFramework - SQLServerExpress i InUseMemory
*/
/* 
    OVISNOST
    - Identity servis
        - Authenticate - na zahtjev traži podatke (gRPC) - NE MOŽE ispuniti zahtjev bez servisa
            - poruka: 503 + tekst
*/
/* 
    OUZP
    - Članak 5. Načela obrade osobnih podataka (39)
    - Članak 6. Zakonitost obrade (40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 155)
    - Članak 7. Uvjeti privole (32, 33, 42, 43)
*/
/* 
    ZAHTJEV
    -   sadrži podatke o entitetu i tko je poslao tj. na što se pretplaćuje
        - sama pretplata i servis na kojem je pretplata mora već postojati - izvor šalje key  
    ODGOVOR
    -   sadrži pozitivan - zaprimljen
    -   sadrži pozitivan - već postoji
    -   sadrži negativan - došlo je do greške
*/
/* 
    KOMUNIKACIJA
    -   zaprima zahtjev  - vanjska sinkrona
    -   šalje da je zahtjev zaprimljen - interna/eksterna asinkrona (MQ)
    -   u DB ima podatke koje ne obrađuje (samo drži)
        -   Service i Suscriptions
            - preko gRPC dostupa do njih  - kada je potrebno inicijalizirati bazu - gRPC
            - sluša MQ ako je došlo do novih podataka ili izmjene trenutnih - MQ
    -   u DB ima podatke koje obrađuje
        - Entity
            - preko gRPC ih čini dostupnom
                - osnovne - za inicijalizaciju baza kod drugih baza - gRPC
                - sve -  za pojedinačni upit - gRPC

*/
/* 
    POBOLJŠANJA
    -   metode izvještaj o pretplatama ()  

*/
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddScoped<ISubscriptionRepo, SubscriptionRepo>();
builder.Services.AddScoped<IIdentityDataClient, IdentityDataClient>();
builder.Services.AddScoped<ILegalDataClient, LegalDataClient>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if(builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("SubscriptionConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseInMemoryDatabase("InMem"));
}
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
Console.WriteLine($"--> RequestProcessService Endpoint {builder.Configuration["RequestProcessService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();

ExceptionMiddlewareExtensions.ConfigureCustomExceptionMiddleware(app);
app.UseAuthorization();
// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
        

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    // global error handler
    //app.UseMiddleware<ErrorHandlerMiddleware>();
    app.MapControllers();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcSubscriptionService>();
 
    endpoints.MapGet("/protos/subscriptions.proto", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/subscriptions.proto"));
    });
});



PublishDb.PublishPopulation(app, builder.Configuration, app.Environment.IsProduction());

//app.MapControllers();

app.Run();
