using LEX_LegalSettings;
using LEX_RequestRecordsService.Models;
using LEX_RequestRecordsService.Models.Authenticate;

namespace LEX_RequestRecordsService.Data;

public interface IRequestRecordsRepo
{
    bool SaveChanges();

    string[] GetLegislationArticleNo();

    #region Request type
    IEnumerable<RequestType> GetAllRequestTypes();    
    bool RequestTypeExists(int requestTypeId);
    RequestType GetRequestTypeById(int requestTypeId);
    RequestType GetRequestTypeByName(string requestTypeName);
    bool ExternalRequestTypeExists(int externalRequestTypeId);
    void CreateRequestType(RequestType requestType);
    //void UpdateRequestType(RequestType requestType);
    #endregion

    #region Request
    IEnumerable<Request> GetAllRequests();
    Request GetRequestById(int? requestTypeId,int requestId);
    IEnumerable<Request> GetRequestByRequestTypeId(int requestTypeId);
    void CreateRequest(int requestTypeId, Request req);
    #endregion

    #region Authentifikacija
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    AuthenticateResponse ValidateToken(string token);
    #endregion

    #region Response
    LegalResponse GetLegalContent(GrpcRequestLegalModel model);
    DefinitionResponse GetLegalDefinitions();
    SubjectDataResponse GetLegalSubjectData();
    LegislationResponse GetLegalLegislations(GrpcRequestLegalModel model);
    #endregion
}