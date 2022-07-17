using LEX_RequestProcessService.Data;
using LEX_RequestProcessService.Models;
using LEX_RequestProcessService.Models.Authenticate;
using LEX_RequestProcessService.SyncDataServices.Grpc;

namespace LEX_RequestProcessService.Data
{
    public class RequestProcessRepo : IRequestProcessRepo
    {
        private readonly IIdentityDataClient _identityservice;
        private readonly AppDbContext _context;

        public RequestProcessRepo(AppDbContext context, IIdentityDataClient identityservice)
        {
            _context = context;
            _identityservice = identityservice;
        }
        #region Entity
        public void CreateEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Entitys.Add(entity);
        }   
        public IEnumerable<Entity> GetAllEntitys()
        {
            return _context.Entitys               
                .OrderBy(s => s.Id);
        }

        public IEnumerable<Entity> GetEntityByKey(string key, string value, ResponseType? responseType)
        {
            if(responseType == null)
            {
                return _context.Entitys.Where(p => p.Email == value).ToList();   
            }

            if(responseType.Id == 1)
            {
                return _context.Entitys.Where(p => p.Email == value).ToList(); 
            }
            else if(responseType.Id == 2)
            {
                // TO DO: dohvati sve Entity podatke iz Subscription servisa za kompleksni upit
                return _context.Entitys.Where(p => p.Email == value).ToList();
            }
            else
            {
                return _context.Entitys.Where(p => p.Email == value).ToList();
            }
            
        }
        public bool ExternalEntityExists(int externalEntityId)
        {
            return _context.Entitys.Any(p => p.ExternalId == externalEntityId);
        }
        #endregion

        #region Request
        public void CreateRequest(Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            _context.Requests.Add(request);
        }
        public IEnumerable<Request> GetAllRequests()
        {
            return (from r in _context.Requests 
                select new Request
                {
                    Id = r.Id,
                    IdentificationKey = r.IdentificationKey,
                    IdentificationString = r.IdentificationString,
                    SourceKey = r.SourceKey,
                    ResponseTypeId = r.ResponseTypeId,
                    ResponseType = _context.ResponseTypes.FirstOrDefault(p => p.Id == r.ResponseTypeId)
                }).ToList();
        }
        public Request GetRequestById(int requestId)
        {
            return _context.Requests.Where(p => p.Id == requestId).FirstOrDefault();
        }
        public Request GetRequestById(int requestTypeId, int requestId)
        {
            return _context.Requests.Where(p => p.Id == requestId && p.ResponseTypeId == requestTypeId).FirstOrDefault();
        }
        public IEnumerable<Entity> GetRequestForEntitys(int subscriptionId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Subscription
        public IEnumerable<Subscription> GetSubscriptions()
        {
            return _context.Subscriptions               
                .OrderBy(s => s.Id);
        }

        public void CreateSubscription(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException(nameof(subscription));
            }
            _context.Subscriptions.Add(subscription);
        }
        public Subscription GetSubscriptionById(int id)
        {
            return _context.Subscriptions.Where(p => p.Id == id).FirstOrDefault();
        }

        public Subscription GetSubscriptionByKey(string key)
        {
            return _context.Subscriptions.Where(p => p.Key == key).FirstOrDefault();
        }        
        
        public bool ExternalSubscriptionExists(int externalSubscriptionId)
        {
            return _context.Subscriptions.Any(p => p.ExternalId == externalSubscriptionId);
        }
        #endregion

        #region Sources
        public bool ExternalSourcesExists(int externalSourcesId)
        {
            return _context.Sources.Any(p => p.ExternalId == externalSourcesId);
        }
        public void CreateSource(Source source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            _context.Sources.Add(source);
        } 
        public bool SourceExists(string key)
        {
            return _context.Sources.Any(p => p.SourceKey == key);
        }
        public Source GetSourceByKey(string sourceKey)
        {
            return _context.Sources.Where(p => p.SourceKey == sourceKey).FirstOrDefault();
        }      
        #endregion

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

        public bool SaveChanges()
        {
            return(_context.SaveChanges() >= 0);
        }
        #region Response & ProcessInfo
        public  IEnumerable<ProcessInfo> GetProcessInfoForEntitys(IEnumerable<Entity> entitys)
        { 
            return (from e in entitys
                select new ProcessInfo
                {
                    EntityId = e.Id,
                    SourceDescription = GetSourceByKey(e.SourceKey).Description,
                    SourceLawfulnessofProcessing = GetSourceByKey(e.SourceKey).LawfulnessProcessing,
                    SourceName = GetSourceByKey(e.SourceKey).Name
                }
            ).ToList();
        }

        public ResponseType GetResponseTypeById(int responseId)
        {
            return _context.ResponseTypes.Where(p => p.Id == responseId).FirstOrDefault();
        }
        #endregion
    }
}