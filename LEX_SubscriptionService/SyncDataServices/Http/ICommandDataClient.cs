using System.Threading.Tasks;
using LEX_SubscriptionService.Dtos;
using LEX_SubscriptionService.Models;
using LEX_SubscriptionService.Models.Authenticate;

namespace LEX_SubscriptionService.SyncDataServices.Http;

public interface ICommandDataClient
{
    Task<HttpResponseMessage>   SendSourcesToRequestProcess(string token,IEnumerable<Source> sourceItem);
}