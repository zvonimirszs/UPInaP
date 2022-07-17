using System.Text.Json;
using AutoMapper;
using Grpc.Net.Client;
using LEX_IdentityService;
//using LEX_IdentityService;
using LEX_LegalSettings.Models;
using LEX_LegalSettings.Models.Authenticate;
using LEX_LegalSettings.SyncDataServices.Grpc;

namespace LEX_SubscriptionService.SyncDataServices.Grpc;
//gRPC Client
public class IdentityDataClient : IIdentityDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public IdentityDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
    public AuthenticateResponse ReturnAuthenticateResponse(AuthenticateRequest model)
    {
        Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcIdentity"]}. ReturnAuthenticateResponse");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcIdentity"]);
        var client = new GrpcIdentity.GrpcIdentityClient(channel);
        var c = new LEX_IdentityService.GrpcUserModel(_mapper.Map<GrpcUserModel>(model));
        var request = new LEX_IdentityService.GetAllRequest();

        try
        {
            var reply = client.Authenticate(c);
            Console.WriteLine(JsonSerializer.Serialize(reply));
            return _mapper.Map<AuthenticateResponse>(reply.Identity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
            return null;
        }
    }

    public AuthenticateResponse ReturnValidateTokenResponse(string token)
    {
        Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcIdentity"]}. ReturnValidateTokenResponse");

        if(token == null)
        {
            return null;
        }
        var channel = GrpcChannel.ForAddress(_configuration["GrpcIdentity"]);
        var client = new GrpcIdentity.GrpcIdentityClient(channel);
        //var c = new LEX_IdentityService.GetAllRequest();
        //c.Token = token;
        var request = new LEX_IdentityService.GetAllRequest();
        request.Token = token;

        try
        {
            var reply = client.ValidateToken(request);
            Console.WriteLine($"--> Reply from Server {JsonSerializer.Serialize(reply)}. ReturnValidateTokenResponse");
            return _mapper.Map<AuthenticateResponse>(reply.Identity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
            return null;
        }
    }
}