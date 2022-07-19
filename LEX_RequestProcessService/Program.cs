using LEX_RequestProcessService.AsyncDataServices;
using LEX_RequestProcessService.Data;
using LEX_RequestProcessService.EventProcessing;
using LEX_RequestProcessService.Helpers;
using LEX_RequestProcessService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/* TO DO: 
    - iz LegalSettings uzeti (gRPC) podatke o voditelju obrade i dpo-u - ulazi u complex odgovor
    - iz LegalSettings uzeti (gRPC) podatke o člancima 6, 12, 13, 14 i 15 - ulazi u complex odgovor
    - iz SubscriptionService uzeti (gRPC) podatke o imenu, prezimenu, adresi, poštanskom broju, opisu  - ulazi u complex odgovor
    - handlati grešku u komunikaciji sa servisima (gRPC i MQ)kada servisi nisu dostupni
*/
/*  ASPEKT/ODGOVORNOST ovog servisa je slanje podataka koje organizacija ima (svi osobni podaci (log)) + priprema odgovora na upit ispitanika vezano za uporabu osobnih podataka:
    - otvoriti metodu za autentifikaciju (prima username i lozinku a vraća token) - token traje 15 minuta
    - zaprimiti zahtjev - 
        - zahtjev mora biti autentificiran tokenom (Bearer)
        - identificirati tko je uputio zahtjev
            - drugi servis, vanjska ili interna aplikacija, mail server 
        - pripremiti odgovor
            - 2 vrste odgovora
                - simple odgovor: 
                    - osnovne podatke o entitetu (), na što je sve pretplaćen, ???? čl. 12 i 13 i 15 -- sadrži u odgovoru osnovne podatke o entitetu i na što je sve pretplaćen(services + subscrptions))
                - complex odgovor: 
                #    - sve podatke o entitetu, na što je sve pretplaćen, ???? čl. 12 i 13 i 15  -- sadrži u odgovoru sve podatke o entitetu i na što je sve pretplaćen(services + subscrptions))   
                #    - podatci o zakonskim člancima + pojašnjenje svake temelj za obradu + definicije
        - poslati asinkronu poruku da je zahtjev zaprimljen - MQ 
    - slušati MQ
    -   u DB ima podatke koje ne obrađuje (samo drži)
        -   Entiti - osnovni podaci - SUBSCRIPTION servis 
            - preko gdpr dostupa do njih - SAMO kada je potrebno inicijalizirati bazu  - sinkrono
            - sluša MQ ako je došlo do novih podataka ili izmjene/brisanja trenutnih
        -   Pretplate/Subcriptions   - SUBSCRIPTION servis 
            - preko gdpr dostupa do njih - SAMO kada je potrebno inicijalizirati bazu  - sinkrono
        #    - sluša MQ ako je došlo do novih podataka ili izmjene/brisanja trenutnih
        -   Usluge/Services - SUBSCRIPTION servis 
            - preko gdpr dostupa do njih - SAMO kada je potrebno inicijalizirati bazu  - sinkrono
        #    - sluša MQ ako je došlo do novih podataka ili izmjene/brisanja trenutnih
*/
/* 
    ZAHTJEV
    -   sadrži identifikaciju riječ ili riječi (emila dresa, koriničko ime usluge, broj korisničkog računa) 
    ODGOVOR
    -   sadrži bazične podatke o obradi (svrha, kategorija podataka...)
    -   sadrži podatke koje postoje tj. koji se obrađuju od strane voditelja obrade
*/
/* 
    FUNKCIONALNOSTI SERVISA
    - API
        - metode za kreiranje i čitanje
        - Authorize i AllowAnonymous
        - Mapiranje (AutoMapper)
    - MQ 
        - šalje
        - prima
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
    - Subscription servis
        - Services - ako nema SVE (gRPC) i sluša dodavanje i izmjene (MQ)
        #    - koju poruku šalje prema van kada servis nije dostupan?
        - Subscriptions - ako nema SVE(gRPC) i sluša dodavanje i izmjene (MQ)
        #    - koju poruku šalje prema van kada servis nije dostupan?
        - Entitys - ako nema SVE(gRPC) i sluša dodavanje i izmjene (MQ)
        #    - koju poruku šalje prema van kada servis nije dostupan?
    - LegalSettings servis
        - SubjectData - na zahtjev traži podatke (gRPC) - MOŽE ispuniti zahtjev bez servisa
        - Definitions  - na zahtjev traži podatke (gRPC) - MOŽE ispuniti zahtjev bez servisa
        - Legal stuff  - na zahtjev traži podatke (gRPC) - MOŽE ispuniti zahtjev bez servisa
    - Identity servis
        - Authenticate - na zahtjev traži podatke (gRPC) - NE MOŽE ispuniti zahtjev bez servisa
        #    - koju poruku šalje prema van kada servis nije dostupan?
*/
/*
    OUZP
    - Članak 12. Transparentne informacije, komunikacija i modaliteti za ostvarivanje prava ispitanika (58, 59, 64)
        - rok za odgovor je 30 dana
    - Članak 13. Informacije koje treba dostaviti ako se osobni podaci prikupljaju od ispitanika (61, 62, 63)
        -   identitet i kontaktne podatke voditelja obrade - SETTINGS
        -   kontaktne podatke službenika za zaštitu podataka - SETTINGS
        -   svrhe obrade - SUBSSETTINGS
        -   pravnu osnovu za obradu - SUBSSETTINGS 
        -   legitimne interese voditelja obrade - ako je obrada po članku 6, stavki 1 - SUBSSETTINGS
        -   primatelje ili kategorije primatelja osobnih podataka - SUBSSETTINGS 
        -   prenijeti trećoj zemlji  DA ili NE - SUBSSETTINGS
        -   razdoblje osobni podaci biti pohranjen - SUBSSETTINGS

        -   mogućnosti: povlačenje prijave, prigovor nadzornom tijelu,  - SETTINGS
    - Članak 14. Informacije koje se trebaju pružiti ako osobni podaci nisu dobiveni od ispitanika
        -   izvor osobnih podataka i, prema potrebi, dolaze li iz javno dostupnih izvora - SUBSSETTINGS 
    - Članak 15. Pravo ispitanika na pristup
        -   kopiju osobnih podataka koji se obrađuju - 
    - Članak 6 - Zakonitost obrade  (40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 155), ISO 27701
        Obrada je zakonita samo ako i u onoj mjeri u kojoj je ispunjeno najmanje jedno od sljedećega: - SUBSSETTINGS
            -   PRIVOLA - (a) ispitanik je dao privolu za obradu svojih osobnih podataka u jednu ili više posebnih svrha;
            -   UGOVORNA OBVEZA - (b) obrada je nužna za izvršavanje ugovora u kojem je ispitanik stranka ili kako bi se poduzele radnje na zahtjev ispitanika prije sklapanja ugovora;
            -   PRAVNE OBVEZE - (c) obrada je nužna radi poštovanja pravnih obveza voditelja obrade;
            -   ZAŠTITA  - (d) obrada je nužna kako bi se zaštitili ključni interesi ispitanika ili druge fizičke osobe; 
            -   JAVNI INTERES - (e) obrada je nužna za izvršavanje zadaće od javnog interesa ili pri izvršavanju službene ovlasti voditelja obrade;
            -   LEGITMNI INTERES - (f) obrada je nužna za potrebe legitimnih interesa voditelja obrade ili treće strane, osim kada su od tih interesa jači interesi ili temeljna prava i slobode ispitanika koji zahtijevaju zaštitu osobnih podataka, osobito ako je ispitanik dijete.
            -   Točka (f) prvog podstavka ne odnosi se na obradu koju provode tijela javne vlasti pri izvršavanju svojih zadaća.
*/
/* 
    POBOLJŠANJA
    #   -   ?
  

*/
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddScoped<IRequestProcessRepo,RequestProcessRepo>();

builder.Services.AddControllers();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>(); 
builder.Services.AddScoped<IIdentityDataClient, IdentityDataClient>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ISubscriptionDataClient, SubscriptionDataClient>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if(builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("RequestProcessConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseInMemoryDatabase("InMem"));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
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

app.MapControllers();

PublishDb.PublishPopulation(app, app.Environment.IsProduction());

app.Run();
