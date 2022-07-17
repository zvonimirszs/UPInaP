using LEX_RequestRecordsService.AsyncDataServices;
using LEX_RequestRecordsService.Data;
using LEX_RequestRecordsService.Helpers;
using LEX_RequestRecordsService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/* TO DO: 
    - pripremiti odgovor nakon zahtjeva
    - iz LegalSettings uzeti (gRPC) podatke o člancima 15, 16, 17, 18, 19, 20 i 21 - ovisno o tipu upita
    - handlati grešku u komunikaciji sa servisima (gRPC i MQ) kada servisi nisu dostupni
    - dodati middleware za handlanje SourceKey-a (IdentityServiceAttribute)
    - primiti poruku da je negdje zahtjev zaprimljen i upisati ga u bazu -  MQ - EventProcessor
*/
/*  ASPEKT/ODGOVORNOST ovog servisa je zaprimanje zahtjeva ispitanika:
    - otvoriti metodu za autentifikaciju (prima username i lozinku a vraća token) - token traje 15 minuta
    - zaprimiti zahtjev
        - zahtjev mora biti autentificiran tokenom (Bearer)
        - identificirati tip zahtjeva/upita 
            - (pravo na pristup, pravo na ispravak, pravo na brisanje, pravo na ograničenje obrade, pravo na prenos podataka, pravo na prigovor)  
    - obraditi zahtjev
        - podaci o tipu zahtjeva - RequestType
    - identificirati tko je uputio zahtjev (preko SourceKey) - na što je pretplata - Service
        - drugi servis, vanjska ili interna aplikacija, mail server - SourceKey se nalazi u Headeru requesta
            - kada nema SourceKey-a 
                - nema pristupa
            - kada  SourceKey-a nije dobar
                - nema pristupa             
    - poslati odgovor
    #    - zahtjev zaprimljen - tekstualni odgovor koji zahvaća pravno tumačenje aspekta zahtjeva     
    - poslati asinkronu poruku da je zahtjev zaprimljen -  MQ
#    - primiti poruku da je negdje zahtjev zaprimljen i upisati ga u bazu -  MQ
    - slušati MQ
    -   u DB ima podatke koje ne obrađuje (samo drži)
        -   Tip zahtjeva (Request type) - LEGALSETTINGS servis 
            - preko gdpr dostupa do njih - SAMO kada je potrebno inicijalizirati bazu  - sinkrono
        #    - sluša MQ ako je došlo do novih podataka ili izmjene/brisanja trenutnih
*/
/* 
    ZAHTJEV
    -   mora imati podataka kada je zaprimljen, da li je otvoren ili zatvoren, opis, napomenu, da li ima followup drugog zahtjeva
    ODGOVOR
    -   sadrži pozitivan - zaprimljen sa tekstom u kojem su stvari vezane uz zakonsku regulativu - nešto kao autoreplay 
    -   sadrži negativan - došlo je do greške
*/
/* 
    FUNKCIONALNOSTI SERVISA
    - API
        - metode za kreiranje i čitanje
        - Authorize i AllowAnonymous
        - Mapiranje (AutoMapper)
    - MQ 
        - šalje
    #    - prima
    - gRPC
        - prima
    - DTO
        - read, published, create
    - Middleware
    - DB
        - EntityFramework - SQLServerExpress i InUseMemory
*/
/* 
    OVISNOST
    - Request Process servis
        - Request - sluša dodavanje i izmjene (MQ)
        #    - sprema u bazu novi zahtjev  - ako ga već nije  - MOŽE ispuniti zahtjev bez servisa
    - LegalSettings servis
        - Legal stuff - RequestType  - na zahtjev traži podatke (gRPC) - MOŽE ispuniti zahtjev bez servisa
    - Identity servis
        - Authenticate - na zahtjev traži podatke (gRPC) - NE MOŽE ispuniti zahtjev bez servisa
        #    - koju poruku šalje prema van kada servis nije dostupan?
*/
/*
    OUZP
     - Članak 15. Pravo ispitanika na pristup (63, 64)
     - Članak 16. Pravo na ispravak (65)
     - Članak 17. Pravo na brisanje („pravo na zaborav”) (65, 66)
     - Članak 18. Pravo na ograničenje obrade (67)
     - Članak 19. Obveza izvješćivanja u vezi s ispravkom ili brisanjem osobnih podataka ili ograničenjem obrade
     - Članak 20. Pravo na prenosivost podataka (68)
     - Članak 21. Pravo na prigovor (69, 70)
*/
/* 
    POBOLJŠANJA
    -   editiranje zahtjeva - od strane internih procesa
    -   povezivanje zahtjeva - od strane internih procesa 

*/


// Add services to the container.
builder.Services.AddCors();
builder.Services.AddScoped<IRequestRecordsRepo, RequestRecordsRepo>();
//builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddScoped<IIdentityDataClient, IdentityDataClient>();
builder.Services.AddScoped<ILegalSettingsDataClient, LegalSettingsDataClient>();
//builder.Services.AddGrpc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if(builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("RequestRecordsConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseInMemoryDatabase("InMem"));
}
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Console.WriteLine($"--> OtherService Endpoint {builder.Configuration["OtherService"]}");

var app = builder.Build();
Console.WriteLine($"--> Environment: {app.Environment.EnvironmentName}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseRouting();

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

    app.MapControllers();
}

PublishDb.PublishPopulation(app, app.Environment.IsProduction());

app.Run();
