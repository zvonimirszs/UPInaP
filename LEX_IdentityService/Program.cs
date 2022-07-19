using LEX_IdentityService.Data;
using LEX_IdentityService.Helpers;
using LEX_IdentityService.IdentityServices;
using LEX_IdentityService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/*  ASPEKT/ODGOVORNOST ovog servisa je autorizacija i atentifikacija zahtjeva po korisničkom imenu i lozinkom te po tokenu :
    - otvoriti metodu za autentifikaciju (prima username i lozinku a vraća token) - token traje 15 minuta
    - zaprimiti zahtjev za autorizacija
        - zahtjev mora imati korisničkom imenu i lozinkom
        - vratiti/odgovor
    - zaprimiti zahtjev za atentifikacija
        - zahtjev mora imati token Bearer
        - vratiti/odgovor
    - identificirati tko je uputio zahtjev (preko SourceKey) - na što je pretplata - Service
        - drugi servis, vanjska ili interna aplikacija, mail server - SourceKey se nalazi u Headeru requesta
*/
/* 
    FUNKCIONALNOSTI SERVISA
    - API
        - NEMA - metode prema van
        - Authorize i AllowAnonymous
        - Mapiranje (AutoMapper)
    - MQ 
        - NEMA
    - gRPC
        - šalje
    - DTO
        - read, published, create
    - Middleware
    - DB
        - Entity Framework - SQLServerExpress i InUseMemory
*/
/* 
    OVISNOST
    - u Produkcijskom okruženju ovisnost o bazi podataka (MS SQL server). u razvojom okruženju se koristi InMemory
*/


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IIdentityRepo, IdentityRepo>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();

if(builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseInMemoryDatabase("InMem"));
}

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
    endpoints.MapGrpcService<GrpcIdentityService>();

    endpoints.MapGet("/protos/identity.proto", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/identity.proto"));
    });
});

 // global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

PublishDb.PublishPopulation(app, builder.Configuration, app.Environment.IsProduction());

app.Run();
