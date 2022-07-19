using System.Text.Json;
using AutoMapper;
using Grpc.Net.Client;
using LEX_RequestProcessService.Models;
using LEX_SubscriptionService;

namespace LEX_RequestProcessService.SyncDataServices.Grpc
{
    /// <summary>
    /// gRPC Client (klijent)
    /// </summary>
    public class SubscriptionDataClient : ISubscriptionDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SubscriptionDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Subscription> ReturnAllSubscriptions()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcSubscription"]}. ReturnAllSubscriptions");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcSubscription"]);
            var client = new GrpcSubscription.GrpcSubscriptionClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllSubscriptions(request);
                return _mapper.Map<IEnumerable<Subscription>>(reply.Subscription);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }

        public IEnumerable<Entity> ReturnAllEntitys()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcSubscription"]}. ReturnAllEntitys");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcSubscription"]);
            var client = new GrpcSubscription.GrpcSubscriptionClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllEntitys(request);
                Console.WriteLine($"--> Reply from Server {JsonSerializer.Serialize(reply)}. ReturnAllEntitys");
                return _mapper.Map<IEnumerable<Entity>>(reply.Entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }  

        public IEnumerable<Source> ReturnAllSources()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcSubscription"]}. ReturnAllSources");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcSubscription"]);
            var client = new GrpcSubscription.GrpcSubscriptionClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllSources(request);
                return _mapper.Map<IEnumerable<Source>>(reply.Source);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }     

    }
}