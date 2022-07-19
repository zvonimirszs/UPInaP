using LEX_RequestRecordsService.Models;
using LEX_RequestRecordsService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace LEX_RequestRecordsService.Data;

public static class PublishDb
{
        public static void PublishPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                MigrateDataBase(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
                // inicijalizacija gRPC Client
                var grpcClient = serviceScope.ServiceProvider.GetService<ILegalSettingsDataClient>();

                var requesttypeItems = grpcClient.ReturnAllRequestTypes();

                SeedDataRequestTypes(serviceScope.ServiceProvider.GetService<IRequestRecordsRepo>(), requesttypeItems);
                SeedDataRequest(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
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
        // samo ako želimo da se svi podaci resetiraju
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

        private static void SeedDataRequestTypes(IRequestRecordsRepo repo, IEnumerable<RequestType> requestTypes)
        {
            Console.WriteLine("Seeding new requestTypes...");
            if(requestTypes != null)
            {
                foreach (var req in requestTypes)
                {
                    if(!repo.ExternalRequestTypeExists(req.ExternalId))
                    {
                        repo.CreateRequestType(req);
                    }
                    repo.SaveChanges();
                }                
            }
            else
            {
                Console.WriteLine("Seeding new requestTypes FAIL: NULL object...");
            }

        }
        private static void SeedDataRequest(AppDbContext context, bool isProd)
        {
            if(!context.Requests.Any())
            {
                //Console.WriteLine($"--> RequestType: {context.RequestTypes.FirstOrDefault(p => p.Id == 1).Name}");
                if(context.RequestTypes.Any())
                {
                    context.Requests.AddRange(
                        new Request() { IdentificationString="zvonimirs.zs@gmail.com", IdentificationKey = $"email", DeliveryKey = $"email", 
                        FirstName="Ime ispitanika", LastName="Prezime ispitanika", Email =  "zvonimirs.zs@gmail.com",
                        Address="Adresa ispitanika", City="Grad ispitanika", PostNo =  "10040",
                        RequestText="Tekst ispitanika", RequestNote="Napomena usluge koja je poslala zahtjev ", StartDate =  DateTime.Now,
                        EndDate = null , RequestTypeId = 1 }
                        );                      
                }                    
                    
                context.SaveChanges();                    
            }
            else
            {
                Console.WriteLine("--> We already have Request data");
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

            // if(!context.RequestTypes.Any())
            // {
            //     Console.WriteLine("--> Seeding Data...");
            //     context.RequestTypes.AddRange(
            //         new RequestType() {Name="Pravo ispitanika na pristup"},
            //         new RequestType() {Name="Pravo na ispravak"},
            //         new RequestType() {Name="Pravo na brisanje"},
            //         new RequestType() {Name="Pravo na prigovor"},
            //         new RequestType() {Name="Pravo na ograničenje obrade"},
            //         //new RequestType() {Name="Obveza izvješćivanja u vezi s ispravkom ili brisanjem osobnih podataka ili ograničenjem obrade"},
            //         new RequestType() {Name="Pravo na prenosivost podataka"},
            //         new RequestType() {Name="Pravo na kopiju osobnih podataka koji se obrađuju"},
            //         new RequestType() {Name="Automatizirano pojedinačno donošenje odluka, uključujući izradu profila"}
            //     );                
            //     context.SaveChanges();
            //     if(!context.Requests.Any())
            //     {
            //         Console.WriteLine($"--> RequestType: {context.RequestTypes.FirstOrDefault(p => p.Id == 1).Name}");
            //         context.Requests.AddRange(
            //             new Request() { IdentificationString="zvonimirs.zs@gmail.com", IdentificationKey = $"email", DeliveryKey = $"email", 
            //             FirstName="Ime ispitanika", LastName="Prezime ispitanika", Email =  "zvonimirs.zs@gmail.com",
            //             Address="Adresa ispitanika", City="Grad ispitanika", PostNo =  "10040",
            //             RequestText="Tekst ispitanika", RequestNote="Napomena usluge koja je poslala zahtjev ", StartDate =  DateTime.Now,
            //             EndDate = null , RequestTypeId = 1 }
            //             );                    
                    
                    
            //         context.SaveChanges();                    
            //     } 
            // }
            // else
            // {
            //     Console.WriteLine("--> We already have data");
            // }
            if(!context.Requests.Any())
            {
                Console.WriteLine($"--> RequestType: {context.RequestTypes.FirstOrDefault(p => p.Id == 1).Name}");
                context.Requests.AddRange(
                    new Request() { IdentificationString="zvonimirs.zs@gmail.com", IdentificationKey = $"email", DeliveryKey = $"email", 
                    FirstName="Ime ispitanika", LastName="Prezime ispitanika", Email =  "zvonimirs.zs@gmail.com",
                    Address="Adresa ispitanika", City="Grad ispitanika", PostNo =  "10040",
                    RequestText="Tekst ispitanika", RequestNote="Napomena usluge koja je poslala zahtjev ", StartDate =  DateTime.Now,
                    EndDate = null , RequestTypeId = 1 }
                    );                    
                    
                    
                context.SaveChanges();                    
            }
            else
            {
                Console.WriteLine("--> We already have Request data");
            }
        }
}