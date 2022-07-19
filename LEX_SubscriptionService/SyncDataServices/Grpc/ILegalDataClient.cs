using LEX_SubscriptionService.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;
using LEX_LegalSettings;

namespace LEX_SubscriptionService.SyncDataServices.Grpc;
public interface ILegalDataClient
{

    LegalResponse ReturnResponse(GrpcRequestLegalModel brojClanaka);

}