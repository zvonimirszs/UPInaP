using LEX_LegalSettings;
using LEX_RequestProcessService.Models;
using LEX_RequestProcessService.Models.Authenticate;

namespace LEX_RequestProcessService.Data;

public interface IRequestProcessRepo
{
    bool SaveChanges();
    string[] GetLegislationArticleNo();
    
    #region Request
    IEnumerable<Request> GetAllRequests();
    Request GetRequestById(int requestTypeId, int requestId);
    Request GetRequestById(int requestId);
    void CreateRequest(Request request);
    #endregion

    #region Entity
    IEnumerable<Entity> GetAllEntitys();
    IEnumerable<Entity> GetRequestForEntitys(int subscriptionId);
    IEnumerable<Entity> GetEntityByKey(string key, string value, ResponseType? responseType);
    void CreateEntity(Entity entity);
    bool ExternalEntityExists(int externalEntityId);
    #endregion

    #region Subscription
    IEnumerable<Subscription> GetSubscriptions();
    Subscription GetSubscriptionById(int id);
    Subscription GetSubscriptionByKey(string key);
    void CreateSubscription(Subscription subscription);
    bool ExternalSubscriptionExists(int externalSubscriptionId);
    #endregion
    
    #region Sources    
    void CreateSource(Source source);
    bool ExternalSourcesExists(int externalSourcesId);
    Source GetSourceByKey(string sourceKey);
    bool SourceExists(string key);
    #endregion

    #region Response & ProcessInfo
    IEnumerable<ProcessInfo> GetProcessInfoForEntitys(IEnumerable<Entity> entitys);
    ResponseType GetResponseTypeById(int responseId);
    DefinitionResponse GetLegalDefinitions();
    SubjectDataResponse GetLegalSubjectData();
    LegislationResponse GetLegalLegislations(GrpcRequestLegalModel model);    
    #endregion

    #region Authentifikacija
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    AuthenticateResponse ValidateToken(string token);
    #endregion

}