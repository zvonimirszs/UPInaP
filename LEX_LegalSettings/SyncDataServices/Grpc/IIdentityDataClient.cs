using LEX_LegalSettings.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace LEX_LegalSettings.SyncDataServices.Grpc;
public interface IIdentityDataClient
{
    AuthenticateResponse ReturnAuthenticateResponse(AuthenticateRequest model);
    AuthenticateResponse ReturnValidateTokenResponse(string token);

}