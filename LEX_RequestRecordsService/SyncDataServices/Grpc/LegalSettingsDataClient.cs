using System.Text.Json;
using AutoMapper;
using Grpc.Net.Client;
using LEX_RequestRecordsService.Models;
using LEX_LegalSettings;

namespace LEX_RequestRecordsService.SyncDataServices.Grpc;
/// <summary>
/// gRPC Client (klijent)
/// </summary>
public class LegalSettingsDataClient : ILegalSettingsDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public LegalSettingsDataClient(IConfiguration configuration, IMapper mapper)
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

}
