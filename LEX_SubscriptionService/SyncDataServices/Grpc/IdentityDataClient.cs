using System.Text.Json;
using AutoMapper;
using Grpc.Net.Client;
using LEX_IdentityService;
using LEX_SubscriptionService.Helpers;
using LEX_SubscriptionService.HelpersExceptionMiddleware.Exceptions;
using LEX_SubscriptionService.Models;
using LEX_SubscriptionService.Models.Authenticate;

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
        Console.WriteLine($"--> Povezivanje na GRPC Servis {_configuration["GrpcIdentity"]}. Metodea: ReturnAuthenticateResponse");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcIdentity"]);
        var client = new GrpcIdentity.GrpcIdentityClient(channel);
        var c = new LEX_IdentityService.GrpcUserModel(_mapper.Map<GrpcUserModel>(model));
        var request = new LEX_IdentityService.GetAllRequest();

        try
        {
            var reply = client.Authenticate(c);
            //Console.WriteLine($"--> Odgovor GRPC Servera: {JsonSerializer.Serialize(reply)}. Metodea: ReturnValidateTokenResponse");
            return _mapper.Map<AuthenticateResponse>(reply.Identity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> NIJE moguće pozvati ili povezati se na GRPC Server {ex.Message}");
            throw new ServiceException($"--> NIJE moguće pozvati ili povezati se na GRPC Server za IDENTIFIKACIJU.");
            //return null;
        }
    }

    public AuthenticateResponse ReturnValidateTokenResponse(string token)
    {
        Console.WriteLine($"--> Povezivanje na GRPC Servis {_configuration["GrpcIdentity"]}. Metodea: ReturnValidateTokenResponse");
        if(token == null)
        {
            return null;
        }
        var channel = GrpcChannel.ForAddress(_configuration["GrpcIdentity"]);
        var client = new GrpcIdentity.GrpcIdentityClient(channel);
        var request = new LEX_IdentityService.GetAllRequest();
        request.Token = token;

        try
        {
            var reply = client.ValidateToken(request);
            //Console.WriteLine($"--> Odgovor GRPC Servera: {JsonSerializer.Serialize(reply)}. Metodea: ReturnValidateTokenResponse");
            return _mapper.Map<AuthenticateResponse>(reply.Identity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> NIJE moguće pozvati ili povezati se na GRPC Server {ex.Message}");
            //throw new AppException($"--> NIJE moguće pozvati ili povezati se na GRPC Server {ex.Message}");
            throw new ServiceException($"--> NIJE moguće pozvati ili povezati se na GRPC Server za IDENTIFIKACIJU.");
        }
    }
}