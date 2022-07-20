using System.Text.Json;
using AutoMapper;
using Grpc.Net.Client;
using LEX_RequestRecordsService.Models;
using LEX_LegalSettings;

namespace LEX_RequestRecordsService.SyncDataServices.Grpc;
/// <summary>
/// gRPC Client (klijent)
/// </summary>
public class LegalDataClient : ILegalDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public LegalDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public IEnumerable<RequestType> ReturnAllRequestTypes()
    {
        Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcLegal"]}. ReturnAllRequestTypes");
        var channel = GrpcChannel.ForAddress(_configuration["GrpcLegal"]);
        var client = new GrpcLegalSetting.GrpcLegalSettingClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllRequestType(request);
            return _mapper.Map<IEnumerable<RequestType>>(reply.Requesttype);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
            return null;
        }
    }

    public DefinitionResponse ReturnDefinitions()
    {
        Console.WriteLine($"--> Povezivanje na GRPC Servis {_configuration["GrpcLegal"]}. Metoda: ReturnResponse");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcLegal"]);
        var client = new GrpcLegalSetting.GrpcLegalSettingClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetDefinition(request);
            Console.WriteLine($"--> Metoda: ReturnDefinitions -- Response: {JsonSerializer.Serialize(reply)}");
            //return _mapper.Map<AuthenticateResponse>(reply);
            return reply;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> NIJE moguće pozvati ili povezati se na GRPC Server {ex.Message}");
            //throw new ServiceException($"--> NIJE moguće pozvati ili povezati se na GRPC Server za ZAKONODAVSTVO.");
            return null;
        }  
    }

    public LegislationResponse ReturnLegislations(GrpcRequestLegalModel model)
    {
        Console.WriteLine($"--> Povezivanje na GRPC Servis {_configuration["GrpcLegal"]}. Metoda: ReturnResponse");
        Console.WriteLine($"--> Metoda: ReturnLegislations -- Request: {JsonSerializer.Serialize(model)}");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcLegal"]);
        var client = new GrpcLegalSetting.GrpcLegalSettingClient(channel);

        try
        {
            var reply = client.GetLegislation(model);
            Console.WriteLine($"--> Metoda: ReturnLegislations -- Response: {JsonSerializer.Serialize(reply)}");
            //return _mapper.Map<AuthenticateResponse>(reply);
            return reply;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> NIJE moguće pozvati ili povezati se na GRPC Server {ex.Message}");
            //throw new ServiceException($"--> NIJE moguće pozvati ili povezati se na GRPC Server za ZAKONODAVSTVO.");
            return null;
        }  
    }

    public LegalResponse ReturnResponse(GrpcRequestLegalModel model)
    {
        Console.WriteLine($"--> Povezivanje na GRPC Servis {_configuration["GrpcLegal"]}. Metoda: ReturnResponse");
        Console.WriteLine($"--> Metoda: ReturnResponse -- Request: {JsonSerializer.Serialize(model)}");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcLegal"]);
        var client = new GrpcLegalSetting.GrpcLegalSettingClient(channel);
        //var c = new LEX_IdentityService.GrpcUserModel(_mapper.Map<GrpcUserModel>(model));
        //var request = new LEX_LegalSettings.GrpcRequestLegalModel();

        try
        {
            var reply = client.GetLegalResponse(model);
            Console.WriteLine($"--> Metoda: ReturnResponse -- Response: {JsonSerializer.Serialize(reply)}");
            //return _mapper.Map<AuthenticateResponse>(reply);
            return reply;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> NIJE moguće pozvati ili povezati se na GRPC Server {ex.Message}");
            //throw new ServiceException($"--> NIJE moguće pozvati ili povezati se na GRPC Server za ZAKONODAVSTVO.");
            return null;
        }       
    }

    public SubjectDataResponse ReturnSubjectData()
    {
        Console.WriteLine($"--> Povezivanje na GRPC Servis {_configuration["GrpcLegal"]}. Metoda: ReturnResponse");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcLegal"]);
        var client = new GrpcLegalSetting.GrpcLegalSettingClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetSubjectData(request);
            Console.WriteLine($"--> Metoda: ReturnSubjectData -- Response: {JsonSerializer.Serialize(reply)}");
            //return _mapper.Map<AuthenticateResponse>(reply);
            return reply;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> NIJE moguće pozvati ili povezati se na GRPC Server {ex.Message}");
            //throw new ServiceException($"--> NIJE moguće pozvati ili povezati se na GRPC Server za ZAKONODAVSTVO.");
            return null;
        }  
    }
}
