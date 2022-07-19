using System.Text.Json;
using LEX_RequestProcessService.Models;
using LEX_RequestProcessService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace LEX_RequestProcessService.Data;

public static class PublishDb
{
    public static void PublishPopulation(IApplicationBuilder app, bool isProd)
    {
        using( var serviceScope = app.ApplicationServices.CreateScope())
        {
            MigrateDataBase(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            // punjenje podataka: tip odgovora i testni zahtjevi
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);

            // inicijalizacija gRPC Client
            var grpcClient = serviceScope.ServiceProvider.GetService<ISubscriptionDataClient>();
            // dohvaćanje svih Pretplata iz drugog servisa putem gRPC
            var subscriptions = grpcClient.ReturnAllSubscriptions();
            // dohvaćanje svih zahtjeva ispitanika za pristupom  iz drugog servisa putem gRPC
            var entitys = grpcClient.ReturnAllEntitys();
            // dohvaćanje svih zahtjeva ispitanika za pristupom  iz drugog servisa putem gRPC
            var sources = grpcClient.ReturnAllSources();

            SeedDataSubscriptions(serviceScope.ServiceProvider.GetService<IRequestProcessRepo>(), subscriptions);
            SeedDataEntitys(serviceScope.ServiceProvider.GetService<IRequestProcessRepo>(), entitys);
            SeedDataSources(serviceScope.ServiceProvider.GetService<IRequestProcessRepo>(), sources);
        }
    }
    private static void MigrateDataBase(AppDbContext context, bool isProd)
    {
        if(isProd)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }
        // samo ako želimo da se zvi podaci resetiraju
        // else
        // {
        //     Console.WriteLine("--> Attempting to apply migrations...");
        //     try
        //     {
        //         context.Database.Migrate();
        //     }
        //     catch(Exception ex)
        //     {
        //         Console.WriteLine($"--> Could not run migrations: {ex.Message}");
        //     }
        // }
    }
    private static void SeedData(AppDbContext context, bool isProd)
    {
        // TO DO: migrirati DBO
        // if(isProd)
        // {
        //     Console.WriteLine("--> Attempting to apply migrations...");
        //     try
        //     {
        //         context.Database.Migrate();
        //     }
        //     catch(Exception ex)
        //     {
        //         Console.WriteLine($"--> Could not run migrations: {ex.Message}");
        //     }
        // }

        // tip odgovora
        if(!context.ResponseTypes.Any())
        {
            Console.WriteLine("--> Seeding Data ResponseTypes...");
                context.ResponseTypes.AddRange(
                    new ResponseType() { Name ="simple" },
                    new ResponseType() { Name ="complex" }
                );                
            context.SaveChanges();
        }
        // testni zahtjevi 
        if(!context.Requests.Any())
        {
            Console.WriteLine("--> Seeding Data Requests...");
                context.Requests.AddRange(
                    new Request() { IdentificationString="zvonimirs.zs@gmail.com", IdentificationKey="email", SourceKey="IUSportal", ResponseTypeId = 1},
                    new Request() { IdentificationString="zvosme21", IdentificationKey="username", SourceKey="INSOLVEportal", ResponseTypeId = 2},
                    new Request() { IdentificationString="KOO322", IdentificationKey="accountkey", SourceKey="NAV", ResponseTypeId = 2}
                );                
            context.SaveChanges();
        }
        else
        {
                Console.WriteLine("--> We already have data");
        }
    }
    private static void SeedDataSubscriptions(IRequestProcessRepo repo, IEnumerable<Subscription> subscriptions)
    {
        Console.WriteLine("Seeding new subscriptions...");

        foreach (var sub in subscriptions)
        {
            if(!repo.ExternalSubscriptionExists(sub.ExternalId))
            {
                repo.CreateSubscription(sub);
            }
            repo.SaveChanges();
        }
    }
    private static void SeedDataEntitys(IRequestProcessRepo repo, IEnumerable<Entity> entitys)
    {
        Console.WriteLine("Seeding new entitys...");

        foreach (var e in entitys)
        {
            Console.WriteLine($"{JsonSerializer.Serialize(e)}");
            if(!repo.ExternalEntityExists(e.ExternalId))
            {
                // TO DO: provjera da li taj Entity već postoji u bazi
                //var subscription = repo.GetSubscriptionByKey(e.);   
                repo.CreateEntity(e);
            }
            repo.SaveChanges();
        }
    }
    private static void SeedDataSources(IRequestProcessRepo repo, IEnumerable<Source> sources)
    {
        Console.WriteLine("Seeding new sources...");

        foreach (var s in sources)
        {
            if(!repo.ExternalSourcesExists(s.ExternalId))
            {
                repo.CreateSource(s);
            }
            repo.SaveChanges();
        }
    }
}