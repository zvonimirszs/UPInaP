using LEX_LegalSettings;
using LEX_RequestRecordsService.Models;
using LEX_RequestRecordsService.Models.Authenticate;
using LEX_RequestRecordsService.SyncDataServices.Grpc;

namespace LEX_RequestRecordsService.Data;
public class RequestRecordsRepo : IRequestRecordsRepo
{
    private readonly AppDbContext _context;       
    private readonly ILegalDataClient _legalservice;
    private readonly IIdentityDataClient _identityservice;  
    private readonly IConfiguration _configuration;
    public RequestRecordsRepo(AppDbContext context, IIdentityDataClient identityservice, ILegalDataClient legalservice, IConfiguration configuration)
    {
        _context = context;
        _identityservice = identityservice;
        _legalservice = legalservice;
        _configuration = configuration;
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

    #region Request
    public IEnumerable<Request> GetAllRequests()
    {
        var requestItems =    (from r in _context.Requests
              select new Request
              {
                   Id = r.Id,
                   IdentificationString = r.IdentificationString,
                   IdentificationKey = r.IdentificationKey,
                   DeliveryKey = r.DeliveryKey,
                   FirstName = r.FirstName,
                   LastName = r.LastName,
                   Email = r.Email,
                   Address = r.Address,
                   City = r.City,
                   PostNo = r.PostNo,
                   RequestText = r.RequestText,
                   RequestNote = r.RequestNote,
                   StartDate = r.StartDate,
                   EndDate = r.EndDate,
                   RequestType =  _context.RequestTypes.FirstOrDefault(p => p.Id == r.RequestTypeId)
              }).ToList();

         return requestItems;
    }
    public IEnumerable<Request> GetRequestByRequestTypeId(int requestTypeId)
    {
        return _context.Requests
                .Where(c => c.RequestTypeId == requestTypeId)
                .OrderBy(c => c.RequestType.Name);
    }

    public void CreateRequest(int requestTypeId, Request req)
    {
        if (req == null)
        {
            throw new ArgumentNullException(nameof(req));
        }

        req.RequestTypeId = requestTypeId;
        _context.Requests.Add(req);
    }  
    public Request GetRequestById(int? requestTypeId, int requestId)
    {
        if( requestTypeId == null)
        {
            return _context.Requests.Where(p => p.Id == requestId).FirstOrDefault();
        }
        else 
        {
            return _context.Requests.Where(p => p.Id == requestId && p.RequestTypeId == requestTypeId).FirstOrDefault();
        }
    }      
    #endregion

    #region Request type
    public IEnumerable<RequestType> GetAllRequestTypes()
    {
        return _context.RequestTypes.ToList();
    }
    public RequestType GetRequestTypeById(int requestTypeId)
    {        
        return _context.RequestTypes.Where(p => p.Id == requestTypeId).FirstOrDefault();
    }  
    public RequestType GetRequestTypeByName(string requestTypeName)
    {        
        return _context.RequestTypes.Where(p => p.Name == requestTypeName).FirstOrDefault();
    } 
    public bool RequestTypeExists(int requestTypeId)
    {
        return _context.RequestTypes.Any(p => p.Id == requestTypeId);
    }
    public bool ExternalRequestTypeExists(int externalRequestTypeId)
    {
        return _context.RequestTypes.Any(p => p.ExternalId == externalRequestTypeId);
    }
    public void CreateRequestType(RequestType requestType)
    {
        if (requestType == null)
        {
            throw new ArgumentNullException(nameof(requestType));
        }
        _context.RequestTypes.Add(requestType);
    }    
    #endregion

    #region Response
    public LegalResponse GetLegalContent(GrpcRequestLegalModel model)
    {
        var grpcClient = _legalservice;
        var response = grpcClient.ReturnResponse(model);

        return response; 
    }    
    public DefinitionResponse GetLegalDefinitions()
    {
        var grpcClient = _legalservice;
        var response = grpcClient.ReturnDefinitions();

        return response; 
    }

    SubjectDataResponse IRequestRecordsRepo.GetLegalSubjectData()
    {
        var grpcClient = _legalservice;
        var response = grpcClient.ReturnSubjectData();

        return response; 
    }

    LegislationResponse IRequestRecordsRepo.GetLegalLegislations(GrpcRequestLegalModel model)
    {
        var grpcClient = _legalservice;
        var response = grpcClient.ReturnLegislations(model);

        return response; 
    }
    #endregion

    public bool SaveChanges()
    {
        return(_context.SaveChanges() >= 0);
    }

    public string[] GetLegislationArticleNo()
    {
        Console.WriteLine($"--> GetLegislationArticleNo: {_configuration["LegislationArticleNo"]}");
        return _configuration["LegislationArticleNo"].Split(',');
    }
}