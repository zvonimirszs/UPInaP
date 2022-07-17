using LEX_LegalSettings.Models;
using LEX_LegalSettings.Models.Authenticate;
using LEX_LegalSettings.SyncDataServices.Grpc;

namespace LEX_LegalSettings.Data;

public class LegalSettingsRepo : ILegalSettingsRepo
{
    private readonly AppDbContext _context;      
    private readonly IIdentityDataClient _identityservice;   
    public LegalSettingsRepo(AppDbContext context, IIdentityDataClient identityservice)
    {
        _context = context;
        _identityservice = identityservice;
    }
    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var grpcClient = _identityservice;
        var auth = grpcClient.ReturnAuthenticateResponse(model);

        return auth; 
    }

    public IEnumerable<RequestType> GetAllRequestTypes()
    {
        return _context.RequestTypes.ToList();
    }

    public RequestType GetRequestType(int requestTypeId)
    {
        return _context.RequestTypes.Where(p => p.Id == requestTypeId).FirstOrDefault();
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public AuthenticateResponse ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}