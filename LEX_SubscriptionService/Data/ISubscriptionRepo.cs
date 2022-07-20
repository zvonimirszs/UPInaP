using System;
using System.Collections.Generic;
using LEX_LegalSettings;
using LEX_SubscriptionService.Models;
using LEX_SubscriptionService.Models.Authenticate;

namespace LEX_SubscriptionService.Data;

public interface ISubscriptionRepo
{
    bool SaveChanges();
    
    string[] GetLegislationArticleNo();
    
    #region Entity
    IEnumerable<Entity> GetAllEntitys();
    IEnumerable<Entity> GetEntitysForSubscription(int subscriptionId);
    Entity GetEntity(int subscriptionId, int entityId);
    Entity GetEntity(int entityId);
    void CreateEntity(int subscriptionId, Entity entity);
    void CreateEntity(Entity entity);
    #endregion

    #region Subscription
    IEnumerable<Subscription> GetAllSubscriptions();
    Subscription GetSubscriptionById(int id);
    void CreateSubscription(Subscription sub);
    void UpdateSubscription(Subscription sub);
    bool SubscriptionExists(int subscriptionId);
    bool SubscriptionExists(string key);
    #endregion

    #region Service
    IEnumerable<Service> GetAllService();
    Service GetServiceById(int id);
    void CreateService(Service sub);
    void UpdateService(Service sub);
    bool ServiceExists(int serviceId);
    #endregion

    #region Source
    IEnumerable<Source> GetAllSource();
    bool SourceExists(string key);
    #endregion

    #region Authentifikacija
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    AuthenticateResponse ValidateToken(string token);
    #endregion

    #region Response
    LegalResponse GetLegalContent(GrpcRequestLegalModel model);
    #endregion

}