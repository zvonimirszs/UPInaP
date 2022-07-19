using LEX_RequestProcessService.Dtos;


/// <summary>
/// Client sučelje - slanje poruka
/// </summary>
namespace LEX_RequestProcessService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewRequestProcess(RequestPublishedDto requestPublishedDto);
    }
}