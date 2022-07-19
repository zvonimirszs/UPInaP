using System.Text.Json;
using AutoMapper;
using Google.Protobuf.Collections;
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

    public override Task<LegalResponse> GetLegalResponse(GrpcRequestLegalModel request, ServerCallContext context)
    {        
        Console.WriteLine($"--> GetLegalResponse, Request: {JsonSerializer.Serialize(request)}...");

        var response = new LegalResponse();
        var subject = _mapper.Map<GrpcSubjectDataModel>(_repository.GetSubject());
        var definitions = _mapper.Map<RepeatedField<GrpcDefinitionModel>>(_repository.GetAllDefinition());
        var legislation = _mapper.Map<RepeatedField<GrpcLegislationModel>>(_repository.GetLegislationByArticleNo(request.ArticleNo));
        
        Console.WriteLine($"--> GetLegalResponse, Query Subject: {JsonSerializer.Serialize(subject)}...");
        Console.WriteLine($"--> GetLegalResponse, Query Definitios: {JsonSerializer.Serialize(definitions)}...");
        Console.WriteLine($"--> GetLegalResponse, Query Legislation: {JsonSerializer.Serialize(legislation)}...");

        response.Subject = subject;
        response.Definitions.Add(definitions);
        response.Legislations.Add(legislation);
        Console.WriteLine("--> Sending Legal response for other services...");
        return Task.FromResult(response);
    }
}