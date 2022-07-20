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

    public override Task<SubjectDataResponse> GetSubjectData(GetAllRequest request, ServerCallContext context)
    {        
        Console.WriteLine($"--> GetSubjectData, Request: {JsonSerializer.Serialize(request)}...");

        var response = new SubjectDataResponse();
        var subject = _mapper.Map<GrpcSubjectDataModel>(_repository.GetSubject());
        
        Console.WriteLine($"--> GetSubjectData, Query Subject: {JsonSerializer.Serialize(subject)}...");

        response.Subject = subject;
        Console.WriteLine("--> Sending SubjectData response for other services...");
        return Task.FromResult(response);
    }

    public override Task<DefinitionResponse> GetDefinition(GetAllRequest request, ServerCallContext context)
    {        
        Console.WriteLine($"--> GetDefinition, Request: {JsonSerializer.Serialize(request)}...");

        var response = new DefinitionResponse();
        var definitions = _mapper.Map<RepeatedField<GrpcDefinitionModel>>(_repository.GetAllDefinition());
        
        Console.WriteLine($"--> GetLegalResponse, Query Definitios: {JsonSerializer.Serialize(definitions)}...");

        response.Definitions.Add(definitions);
        Console.WriteLine("--> Sending Definitions response for other services...");
        return Task.FromResult(response);
    }

    public override Task<LegislationResponse> GetLegislation(GrpcRequestLegalModel request, ServerCallContext context)
    {        
        Console.WriteLine($"--> GetLegislation, Request: {JsonSerializer.Serialize(request)}...");

        var response = new LegislationResponse();
        var legislation = _mapper.Map<RepeatedField<GrpcLegislationModel>>(_repository.GetLegislationByArticleNo(request.ArticleNo));
        
        Console.WriteLine($"--> GetLegalResponse, Query Legislation: {JsonSerializer.Serialize(legislation)}...");

        response.Legislations.Add(legislation);
        Console.WriteLine("--> Sending Legislation response for other services...");
        return Task.FromResult(response);
    }
}