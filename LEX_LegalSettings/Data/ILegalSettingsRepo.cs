using LEX_LegalSettings.Models;
using LEX_LegalSettings.Models.Authenticate;

namespace LEX_LegalSettings.Data;

public interface ILegalSettingsRepo
{
    bool SaveChanges();
    
    #region RequestType
    IEnumerable<RequestType> GetAllRequestTypes();
    RequestType GetRequestType(int requestTypeId);
    #endregion

    #region Definitions
    IEnumerable<Definition> GetAllDefinition();
    #endregion

    #region Subject
    SubjectData GetSubject();
    #endregion

    #region Legislation
    IEnumerable<Legislation> GetAllLegislation();
    IEnumerable<Legislation> GetLegislationByArticleNo(IList<string> articleNos);
    #endregion

    #region Authentifikacija
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    AuthenticateResponse ValidateToken(string token);
    #endregion

}