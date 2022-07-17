using LEX_LegalSettings.Data;
using LEX_LegalSettings.SyncDataServices.Grpc;
using LEX_SubscriptionService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/* TO DO: 
    - handlati grešku u komunikaciji sa servisima (gRPC i MQ) kada servisi nisu dostupni
    - pripremiti metode za slanje SubjectData, Definitions, Legislation
    - doraditi gRPC model za slanje SubjectData i Legislation
    - doraditi controller za slanje Definitions
*/
/*  ASPEKT/ODGOVORNOST ovog servisa je domena pravih tekstova i postavke vidotelja obrade:
   # - poslati asinkronu poruku da se nešto izmjenilo ili dodalo (MQ)
    - omogućiti pristup podacima preko gRPC-a 
*/
/* 
    FUNKCIONALNOSTI SERVISA
    - API
        - NEMA moetoda prema vani 
        - Authorize i AllowAnonymous
        - Mapiranje (AutoMapper)
    - MQ 
        - šalje
    - gRPC
        - šalje
    - DTO
        - read, published, create
    - Middleware
    - DB
        - EntityFramework - InUseMemory
*/
/* 
    OVISNOST
    - Identity servis
        - Authenticate - na zahtjev traži podatke (gRPC) - NE MOŽE ispuniti zahtjev bez servisa
        #    - koju poruku šalje prema van kada servis nije dostupan?
*/
/* 
    POBOLJŠANJA
    -   kada se nešto izmjeni na requesttype, subjectdata, definicijama - trebalo bi poslati poruku (MQ)
        - MQ
            - slati
*/

// Add services to the container.
builder.Services.AddScoped<ILegalSettingsRepo, LegalSettingsRepo>();
builder.Services.AddScoped<IIdentityDataClient, IdentityDataClient>();
builder.Services.AddGrpc();

Console.WriteLine("--> Using InMem Db");
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseInMemoryDatabase("InMem"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Console.WriteLine($"--> Environment: {app.Environment.EnvironmentName}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcLegalSettingsService>();
 
    endpoints.MapGet("/protos/legal.proto", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/legal.proto"));
    });
});

//app.MapControllers();

PublishDb.PublishPopulation(app,  builder.Configuration, app.Environment.IsProduction());

app.Run();
