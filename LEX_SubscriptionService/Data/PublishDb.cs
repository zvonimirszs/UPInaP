using System.Text.Json;
using LEX_SubscriptionService.Models;
using Microsoft.EntityFrameworkCore;

namespace LEX_SubscriptionService.Data;

public static class PublishDb
{
    public static void PublishPopulation(IApplicationBuilder app, ConfigurationManager config,bool isProd)
    {
        using( var serviceScope = app.ApplicationServices.CreateScope())
        {
            MigrationData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            SeedServicesData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);
            SeedSourceData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);
            SeedSubscriptionData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);

            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
        }
    }
    // TO DO: Migracija
    private static void MigrationData(AppDbContext context, bool isProd)
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
    // preuzimanje podataka o Servisima iz Json dokumenta
    private static void SeedServicesData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.Services.Any())
        {
            Console.WriteLine($"--> Reading from file {config["ServicesData"]} Data ServicesData...");
            List<Models.Service> serviceTypeItems = new List<Models.Service>();
            using (StreamReader r = new StreamReader(config["ServicesData"]))
            {
                string json = r.ReadToEnd();
                serviceTypeItems = JsonSerializer.Deserialize<List<Models.Service>>(json);
            }
            Console.WriteLine("--> Seeding Data ServicesData...");
            foreach (var service in serviceTypeItems)
            {
                context.Services.AddRange(service); 
            }
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have ServicesData data");
        }
    }
    // preuzimanje podataka o Subcrptions iz Json dokumenta
    private static void SeedSubscriptionData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.Subscriptions.Any())
        {
            Console.WriteLine($"--> Reading from file {config["SubscriptionData"]} Data SubscriptionData...");
            List<Models.Subscription> subscriptionItems = new List<Models.Subscription>();
            using (StreamReader r = new StreamReader(config["SubscriptionData"]))
            {
                string json = r.ReadToEnd();
                subscriptionItems = JsonSerializer.Deserialize<List<Models.Subscription>>(json);
            }
            Console.WriteLine("--> Seeding Data SubscriptionData...");
            foreach (var sub in subscriptionItems)
            {
                context.Subscriptions.AddRange(sub); 
            }
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have SubscriptionData data");
        }
    }
    // preuzimanje podataka o Sources iz Json dokumenta
    private static void SeedSourceData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.Sources.Any())
        {
            Console.WriteLine($"--> Reading from file {config["SourceData"]} Data SourceData...");
            List<Models.Source> sourceItems = new List<Models.Source>();
            using (StreamReader r = new StreamReader(config["SourceData"]))
            {
                string json = r.ReadToEnd();
                sourceItems = JsonSerializer.Deserialize<List<Models.Source>>(json);
            }
            Console.WriteLine("--> Seeding Data SourceData...");
            foreach (var source in sourceItems)
            {
                context.Sources.AddRange(source); 
            }
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have SourceData data");
        }
    }

    private static void SeedData(AppDbContext context, bool isProd)
    {
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
        
        // if(!context.Services.Any())
        // {
        //     Console.WriteLine("--> Seeding Data...");

        //     context.Services.AddRange(
        //         new Service() {Name="IUS-INFO", Description="Pravni sadrzaj IUS-INFO"},
        //         new Service() {Name="INSOLVE"},
        //         new Service() {Name="Dogadaji", Description="Dogadaji LEXPERA"},
        //         new Service() {Name="Knjige", Description="Knjige i e-knjige LEXPERA"}
        //     );                
        //     context.SaveChanges();
        //     if(context.Services.Any())
        //     {
        //         Console.WriteLine($"--> Service: {context.Services.FirstOrDefault(p => p.Id == 1).Name}");
        //         context.Subscriptions.AddRange(
        //             new Subscription() {Name="Glasnik", ServiceId = 1, Key = $"Glasnik{context.Services.FirstOrDefault(p => p.Id == 1).Name}", Purpose="Svrha obrade ???", Description="Tjedne obavijesti na e-mail", Service =  context.Services.FirstOrDefault(p => p.Id == 1)},
        //             new Subscription() {Name="Oglasavanje", ServiceId = 1,Key = $"Oglasavanje{context.Services.FirstOrDefault(p => p.Id == 1).Name}", Purpose="Svrha obrade ???", Description="Obavijesti na e-mail vezano uz pravni sadržaj", Service =  context.Services.FirstOrDefault(p => p.Id == 1)},
        //             new Subscription() {Name="Glasnik", ServiceId = 2,Key = $"Glasnik{context.Services.FirstOrDefault(p => p.Id == 2).Name}", Purpose="Svrha obrade ???",  Description="Mjesečne obavijesti na e-mail", Service =  context.Services.FirstOrDefault(p => p.Id == 2)},
        //             new Subscription() {Name="Novosti", ServiceId = 3,Key = $"Novosti{context.Services.FirstOrDefault(p => p.Id == 3).Name}", Purpose="Svrha obrade ???",  Description="Obavijesti o novim događajima na e-mail", Service =  context.Services.FirstOrDefault(p => p.Id == 3)},
        //             new Subscription() {Name="Novosti-postom", ServiceId = 3,Key = $"Novosti-postom{context.Services.FirstOrDefault(p => p.Id == 3).Name}", Purpose="Svrha obrade ???",  Description="Obavijesti o novim događajima slanje poštom na adresu", Service =  context.Services.FirstOrDefault(p => p.Id == 3)}
        //         );
        //         context.SaveChanges();                    
        //     } 
            
        // }
        // else
        // {
        //     Console.WriteLine("--> We already have data");
        // }
        if(context.Subscriptions.Any())
        {
            Console.WriteLine($"--> Subscriptions: {context.Subscriptions.FirstOrDefault(p => p.Id == 1).Name}");
            context.Entitys.AddRange(
                new Entity() {FirstName = "Zvonimir", LastName = "Šmer", Email="zvonimirs.zs@gmail.com", Address="Moja adresa 11", 
                City="Zagreb", PostNo="10000", Description="Opis - nečega", SourceKey = "IUSPortalRegistracija",
                Subscription =  context.Subscriptions.FirstOrDefault(p => p.Id == 1)},
                new Entity() {FirstName = "Zvonimir", LastName = "Šmer", Email="zvonimirs.zs@gmail.com", Address="Moja adresa 11", 
                City="Zagreb", PostNo="10000", Description="Opis - nečega", SourceKey = "INSOLVEPortalRegistracija",
                Subscription =  context.Subscriptions.FirstOrDefault(p => p.Id == 3)},
                new Entity() {FirstName = "Ivan", LastName = "Horfat", Email="mojadresa@gmail.com", Address="Moja adresa 11", 
                City="Zagreb", PostNo="10000", Description="Opis - nečega", SourceKey = "NAVIUSPretplata",
                Subscription =  context.Subscriptions.FirstOrDefault(p => p.Id == 1)}
        
                );
            context.SaveChanges();                    
        } 
    }
    
}
public static class JsonFileReader
{
    public static T Read<T>(string filePath)
    {
        string text = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(text);
    }
}