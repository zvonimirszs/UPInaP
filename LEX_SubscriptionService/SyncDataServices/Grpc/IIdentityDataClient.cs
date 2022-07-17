using LEX_SubscriptionService.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace LEX_SubscriptionService.SyncDataServices.Grpc;
public interface IIdentityDataClient
{
    AuthenticateResponse ReturnAuthenticateResponse(AuthenticateRequest model);
    AuthenticateResponse ReturnValidateTokenResponse(string token);

}