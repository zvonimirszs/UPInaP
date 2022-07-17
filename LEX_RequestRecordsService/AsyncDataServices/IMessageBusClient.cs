using LEX_RequestRecordsService.Dtos;

namespace LEX_RequestRecordsService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewRequestRecord(RequestPublishedDto requestPublishedDto);
    }
}