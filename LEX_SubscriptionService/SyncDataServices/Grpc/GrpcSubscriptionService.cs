using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using LEX_SubscriptionService.Data;
using System.Text.Json;

namespace LEX_SubscriptionService.SyncDataServices.Grpc;
// gRPC Server
public class GrpcSubscriptionService : GrpcSubscription.GrpcSubscriptionBase
{
    private readonly ISubscriptionRepo _repository;
    private readonly IMapper _mapper;

    public GrpcSubscriptionService(ISubscriptionRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override Task<SubscriptionResponse>  GetAllSubscriptions(GetAllRequest request, ServerCallContext context)
    {        
        var response = new SubscriptionResponse();
        var subscriptions = _repository.GetAllSubscriptions();

        foreach(var sub in subscriptions)
        {                
            response.Subscription.Add(_mapper.Map<GrpcSubscriptionModel>(sub));
        }
        Console.WriteLine("--> Sending Subscriptions For other services...");
        return Task.FromResult(response);
    }

    public override Task<EntityResponse> GetAllEntitys(GetAllRequest request, ServerCallContext context)
    {            
        var response = new EntityResponse();
        var entityItems = _repository.GetAllEntitys();

        foreach(var ent in entityItems)
        {
            //Console.WriteLine($"--> Send to Client GetAllEntitys {JsonSerializer.Serialize(ent)}");
            //Console.WriteLine($"--> Send to Client GetAllEntitys {JsonSerializer.Serialize(_mapper.Map<GrpcEntityModel>(ent))}");
            response.Entity.Add(_mapper.Map<GrpcEntityModel>(ent));
        }
        Console.WriteLine("--> Sending Entity For other services...");
        return Task.FromResult(response);
    }

    public override Task<SourceResponse> GetAllSources(GetAllRequest request, ServerCallContext context)
    {            
        var response = new SourceResponse();
        var sourcesItems = _repository.GetAllSource();

        foreach(var source in sourcesItems)
        {
            response.Source.Add(_mapper.Map<GrpcSourceModel>(source));
        }
        Console.WriteLine("--> Sending Source For other services...");
        return Task.FromResult(response);
    }
}