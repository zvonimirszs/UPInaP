using LEX_LegalSettings;
using LEX_SubscriptionService.Models;
using LEX_SubscriptionService.Models.Authenticate;
using LEX_SubscriptionService.SyncDataServices.Grpc;

namespace LEX_SubscriptionService.Data
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private readonly AppDbContext _context;
        private readonly IIdentityDataClient _identityservice;
        private readonly ILegalDataClient _legalservice;
        private readonly IConfiguration _configuration;

        public SubscriptionRepo(AppDbContext context, IIdentityDataClient identityservice, ILegalDataClient legalservice, IConfiguration configuration)
        {
            _context = context;
            _identityservice = identityservice;
            _legalservice = legalservice;
            _configuration = configuration;
        }
        public bool SaveChanges()
        {
            return(_context.SaveChanges() >= 0);
        }
        public string[] GetLegislationArticleNo()
        {
            Console.WriteLine($"--> GetLegislationArticleNo: {_configuration["LegislationArticleNo"]}");
            return _configuration["LegislationArticleNo"].Split(',');
        }

        #region Authentifikacija
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var grpcClient = _identityservice;
            var auth = grpcClient.ReturnAuthenticateResponse(model);

            return auth;    
        }

        public AuthenticateResponse ValidateToken(string token)
        {
            var grpcClient = _identityservice;
            var auth = grpcClient.ReturnValidateTokenResponse(token);

            return auth;
        }
        #endregion
        
        #region Entity
        public void CreateEntity(int subscriptionId, Entity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.SubscriptionId = subscriptionId;
            _context.Entitys.Add(entity);            
        }

        public void CreateEntity(Entity entity)
        {
             if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }            
            _context.Entitys.Add(entity);  
        }
        public IEnumerable<Entity> GetAllEntitys()
        {
             return (from e in _context.Entitys
              select new Entity
              {
                   Id = e.Id,
                   FirstName = e.FirstName,
                   LastName = e.LastName,
                   Email = e.Email,
                    Address = e.Address,
                    City = e.City,
                    PostNo = e.PostNo,
                    Description = e.Description,
                    SourceKey = e.SourceKey,
                    SubscriptionId = e.SubscriptionId,
                    Subscription =  _context.Subscriptions.FirstOrDefault(p => p.Id == e.SubscriptionId)
              }
            ).ToList();
        }      
        public Entity GetEntity(int entityId)
        {
            var entityItem = _context.Entitys
                .Where(c => c.Id == entityId).FirstOrDefault();
            if(entityItem != null)
            {
                entityItem.Subscription = _context.Subscriptions.FirstOrDefault(p => p.Id == entityItem.SubscriptionId);                
            }
            return entityItem;
        }
        public Entity GetEntity(int subscriptionId, int entityId)
        {
            var entityItem = _context.Entitys
                .Where(c => c.SubscriptionId == subscriptionId && c.Id == entityId).FirstOrDefault();
            if(entityItem != null)
            {
                entityItem.Subscription = _context.Subscriptions.FirstOrDefault(p => p.Id == entityItem.SubscriptionId);                
            }
            return entityItem;
        }

        public IEnumerable<Entity> GetEntitysForSubscription(int subscriptionId)
        {
            return (from e in _context.Entitys
              select new Entity
              {
                   Id = e.Id,
                   FirstName = e.FirstName,
                   LastName = e.LastName,
                   Email = e.Email,
                   Address = e.Address,
                   City = e.City,
                   PostNo = e.PostNo,
                   Description = e.Description,
                   SourceKey = e.SourceKey,
                   SubscriptionId = e.SubscriptionId,
                   Subscription =  _context.Subscriptions.FirstOrDefault(p => p.Id == e.SubscriptionId)
              }
            ).Where(c => c.SubscriptionId == subscriptionId).ToList();
        }
        #endregion

        #region Service  
        public IEnumerable<Service> GetAllService()
        {
            throw new NotImplementedException();
        }              
        public void CreateService(Service sub)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Service> GetAllServices()
        {
            throw new NotImplementedException();
        }        
        public Service GetServiceById(int id)
        {
            throw new NotImplementedException();
        }       
        public bool ServiceExists(int serviceId)
        {
            return _context.Services.Any(p => p.Id == serviceId);
        }
        public void UpdateService(Service sub)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Subscription
        public void CreateSubscription(Subscription sub)
        {
            throw new NotImplementedException();
            
        }
        public IEnumerable<Subscription> GetAllSubscriptions()
        {
              var subscriptionItem =    (from s in _context.Subscriptions
              select new Subscription
              {
                   Id = s.Id,
                   Name = s.Name,
                   Key = s.Key,
                   Purpose = s.Purpose,
                   Description = s.Description,
                   ServiceId = s.ServiceId,
                   Service =  _context.Services.FirstOrDefault(p => p.Id == s.ServiceId)
              }).ToList();

             //return _context.Subscriptions.ToList();
             return subscriptionItem;
        }
        public Subscription GetSubscriptionById(int id)
        {
            return _context.Subscriptions.FirstOrDefault(p => p.Id == id);
        }     
        public bool SubscriptionExists(int subscriptionId)
        {
            return _context.Subscriptions.Any(p => p.Id == subscriptionId);
        }

        public bool SubscriptionExists(string key)
        {
            return _context.Subscriptions.Any(p => p.Key == key);
        }
        public void UpdateSubscription(Subscription sub)
        {
            throw new NotImplementedException();
        }           
        #endregion

        #region Source
        public IEnumerable<Source> GetAllSource()
        {
            var sourcesItem = _context.Sources.ToList();

             return sourcesItem;
        }

        public bool SourceExists(string key)
        {
            return _context.Sources.Any(p => p.SourceKey == key);
        }


        #endregion

        #region Response
        public LegalResponse GetLegalContent(GrpcRequestLegalModel model)
        {
            var grpcClient = _legalservice;
            var response = grpcClient.ReturnResponse(model);

            return response; 
        }
        #endregion


    }
}