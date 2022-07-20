using LEX_SubscriptionService.Dtos;

namespace LEX_SubscriptionService.AsyncDataServices
{
    /// <summary>
    /// Sučelje IMessageBusClient
    /// </summary>
    public interface IMessageBusClient
    {
        
        void PublishNewSubcription(SubscriptionPublishedDto subscriptionPublishedDto);

        void PublishNewEntitySubcription(EntityPublishedDto entityPublishedDto);
    }
}