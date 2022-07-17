using AutoMapper;
using Grpc.Core;
using LEX_LegalSettings.Data;

namespace LEX_LegalSettings.SyncDataServices.Grpc;
// gRPC Server
public class GrpcLegalSettingsService : GrpcLegalSetting.GrpcLegalSettingBase
{
    private readonly ILegalSettingsRepo _repository;
    private readonly IMapper _mapper;

    public GrpcLegalSettingsService(ILegalSettingsRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override Task<RequestTypeResponse> GetAllRequestType(GetAllRequest request, ServerCallContext context)
    {        
        var response = new RequestTypeResponse();
        var requestTypeItems = _repository.GetAllRequestTypes();

        foreach(var req in requestTypeItems)
        {                
            response.Requesttype.Add(_mapper.Map<GrpcRequestTypeModel>(req));
        }
        Console.WriteLine("--> Sending RequestTypes For other services...");
        return Task.FromResult(response);
    }
}