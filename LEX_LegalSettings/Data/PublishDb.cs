using System.Text.Json;
using LEX_LegalSettings.Models;

namespace LEX_LegalSettings.Data;
public static class PublishDb
{
    public static void PublishPopulation(IApplicationBuilder app, ConfigurationManager config, bool isProd)
    { 
        
        using( var serviceScope = app.ApplicationServices.CreateScope())
        {            
            SeedSubjectData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);
            SeedRequestTypeData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);
            SeedLawfulnessProcessingData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);
            SeedDefinitionData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);
            SeedLegislationData(serviceScope.ServiceProvider.GetService<AppDbContext>(), config, isProd);
        }
    }
    private static void SeedSubjectData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.SubjectDatas.Any())
        {
            Console.WriteLine($"--> Reading from file {config["SubjectDataData"]} Data SubjectData...");
            SubjectData subjectDataItem = JsonFileReader.Read<SubjectData>(config["SubjectDataData"]);
            Console.WriteLine("--> Seeding Data SubjectData...");
            context.SubjectDatas.AddRange(subjectDataItem); 
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have SubjectData data");
        }
    }
    private static void SeedRequestTypeData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.RequestTypes.Any())//"Json/RequestTypes.json"
        {
            Console.WriteLine($"--> Reading from file {config["RequestTypesData"]} Data RequestTypes...");
            List<RequestType> requestTypeItems = new List<RequestType>();
            using (StreamReader r = new StreamReader(config["RequestTypesData"]))
            {
                string json = r.ReadToEnd();
                requestTypeItems = JsonSerializer.Deserialize<List<RequestType>>(json);
            }
            Console.WriteLine("--> Seeding Data RequestTypes...");
            foreach (var r in requestTypeItems)
            {
                context.RequestTypes.AddRange(r); 
            }
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have RequestTypes data");
        }
        

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
        // if(!context.ResponseTypes.Any())
        // {
        //     Console.WriteLine("--> Seeding Data ResponseTypes...");
        //         context.ResponseTypes.AddRange(
        //             new ResponseType() { Name ="simple" },
        //             new ResponseType() { Name ="complex" }
        //         );                
        //     context.SaveChanges();
        // }
        // if(!context.Requests.Any())
        // {
        //     Console.WriteLine("--> Seeding Data Requests...");
        //         context.Requests.AddRange(
        //             new Request() { IdentificationString="zvonimirs.zs@gmail.com", IdentificationKey="email", SourceKey="IUSportal", ResponseTypeId = 1},
        //             new Request() { IdentificationString="zvosme21", IdentificationKey="username", SourceKey="INSOLVEportal", ResponseTypeId = 2},
        //             new Request() { IdentificationString="KOO322", IdentificationKey="accountkey", SourceKey="NAV", ResponseTypeId = 2}
        //         );                
        //     context.SaveChanges();
        // }
        // else
        // {
        //         Console.WriteLine("--> We already have data");
        // }
    }
    private static void SeedLawfulnessProcessingData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.LawfulnessProcessings.Any())
        {
            Console.WriteLine($"--> Reading from file {config["LawfulnessProcessingData"]} Data LawfulnessProcessing...");
            List<LawfulnessProcessing> lawfulnessProcessingItems = new List<LawfulnessProcessing>();
            using (StreamReader r = new StreamReader(config["LawfulnessProcessingData"]))
            {
                string json = r.ReadToEnd();
                lawfulnessProcessingItems = JsonSerializer.Deserialize<List<LawfulnessProcessing>>(json);
            }
            Console.WriteLine("--> Seeding Data LawfulnessProcessing...");
            foreach (var lp in lawfulnessProcessingItems)
            {
                context.LawfulnessProcessings.AddRange(lp); 
            }
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have LawfulnessProcessing data");
        }
    }

    private static void SeedDefinitionData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.Definitions.Any())
        {
            Console.WriteLine($"--> Reading from file {config["DefinitionData"]} Data Definition...");
            List<Definition> definitionItems = new List<Definition>();
            using (StreamReader r = new StreamReader(config["DefinitionData"]))
            {
                string json = r.ReadToEnd();
                definitionItems = JsonSerializer.Deserialize<List<Definition>>(json);
            }
            Console.WriteLine("--> Seeding Data Definition...");
            foreach (var def in definitionItems)
            {
                context.Definitions.AddRange(def); 
            }
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have Definition data");
        }
    }

    private static void SeedLegislationData(AppDbContext context, ConfigurationManager config, bool isProd)
    {
        if(!context.Legislations.Any())
        {
            Console.WriteLine($"--> Reading from file {config["LegislationData"]} Data Legislation...");
            List<Legislation> legislationItems = new List<Legislation>();
            using (StreamReader r = new StreamReader(config["LegislationData"]))
            {
                string json = r.ReadToEnd();
                legislationItems = JsonSerializer.Deserialize<List<Legislation>>(json);
            }
            Console.WriteLine("--> Seeding Data Legislation...");
            foreach (var def in legislationItems)
            {
                context.Legislations.AddRange(def); 
            }
            context.SaveChanges();            
        }
        else
        {
            Console.WriteLine("--> We already have Legislation data");
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