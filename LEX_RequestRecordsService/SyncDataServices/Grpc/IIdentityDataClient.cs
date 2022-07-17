using LEX_RequestRecordsService.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace LEX_RequestRecordsService.SyncDataServices.Grpc;
public interface IIdentityDataClient
{
    AuthenticateResponse ReturnAuthenticateResponse(AuthenticateRequest model);
    AuthenticateResponse ReturnValidateTokenResponse(string token);

}