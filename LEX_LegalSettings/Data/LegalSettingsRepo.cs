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
    public bool SaveChanges()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
    #endregion

    #region RequestType
    public IEnumerable<RequestType> GetAllRequestTypes()
    {
        return _context.RequestTypes.ToList();
    }

    public RequestType GetRequestType(int requestTypeId)
    {
        return _context.RequestTypes.Where(p => p.Id == requestTypeId).FirstOrDefault();
    }
    #endregion


    #region Definitions
    public IEnumerable<Definition> GetAllDefinition()
    {
         return _context.Definitions.ToList();
    }
    #endregion

    #region Subject
    public SubjectData GetSubject()
    {
        return _context.SubjectDatas.FirstOrDefault();
    }
    #endregion

    #region Legislation
    public IEnumerable<Legislation> GetAllLegislation()
    {
         return _context.Legislations.ToList();
    }
    public IEnumerable<Legislation> GetLegislationByArticleNo(IList<string> articleNos)
    {
        return _context.Legislations.Where(l => articleNos.Contains(l.ArticleNo)).ToList();
    }
    #endregion




}