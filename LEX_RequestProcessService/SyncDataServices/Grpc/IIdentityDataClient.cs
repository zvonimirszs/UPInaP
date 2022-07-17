using LEX_RequestProcessService.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace LEX_RequestProcessService.SyncDataServices.Grpc;
public interface IIdentityDataClient
{
    AuthenticateResponse ReturnAuthenticateResponse(AuthenticateRequest model);
    AuthenticateResponse ReturnValidateTokenResponse(string token);

}