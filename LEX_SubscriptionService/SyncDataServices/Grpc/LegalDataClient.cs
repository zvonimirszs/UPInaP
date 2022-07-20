using System.Text.Json;
using AutoMapper;
using Grpc.Net.Client;
using LEX_IdentityService;
using LEX_LegalSettings;
using LEX_SubscriptionService.Helpers;
using LEX_SubscriptionService.HelpersExceptionMiddleware.Exceptions;
using LEX_SubscriptionService.Models;
using LEX_SubscriptionService.Models.Authenticate;

namespace LEX_SubscriptionService.SyncDataServices.Grpc;
//gRPC Client
public class LegalDataClient : ILegalDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public LegalDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
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
}