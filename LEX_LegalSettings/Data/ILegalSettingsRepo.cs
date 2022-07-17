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

    #region Authentifikacija
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    AuthenticateResponse ValidateToken(string token);
    #endregion

}