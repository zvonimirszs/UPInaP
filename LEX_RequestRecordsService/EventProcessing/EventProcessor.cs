using System.Text.Json;
using AutoMapper;
using LEX_RequestRecordsService.Data;
using LEX_RequestRecordsService.Dtos;
using LEX_RequestRecordsService.Models;


namespace LEX_RequestRecordsService.EventProcessing;
/// <summary>
/// Server klasa - konzumacija događaja
/// </summary>
public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
    {
        Console.WriteLine("--> Event contructor");

        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }
    /// <summary>
    /// Konzumacija događaja
    /// </summary>
    /// <param name="message">primljena poruka</param>
    public void ProcessEvent(string message)
    {
        Console.WriteLine("--> Process Event");
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.RequestPublished:
                addRequest(message);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Koji je događaj
    /// </summary>
    /// <param name="notifcationMessage">primljena poruka</param>
    private EventType DetermineEvent(string notifcationMessage)
    {
        Console.WriteLine("--> Determining Event");

        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

        switch(eventType.Event)
        {
            case "EntitySubscription_Published":
                Console.WriteLine("--> Entity Published Event Detected");
                return EventType.EntityPublished;
            case "Subcription_Published":
                Console.WriteLine("--> Subcription Published Event Detected");
                return EventType.SubscriptionPublished;
            case "RequestProcess_Published":
                Console.WriteLine("--> RequestProcess Published Event Detected");
                return EventType.RequestPublished;
            default:
                Console.WriteLine("--> Could not determine the event type");
                return EventType.Undetermined;
        }
    }
    // private void addRequest(string entityPublishedMessage)
    // {

    // }
    /// <summary>
    /// Dodavanje Request-a pravo ispitanika
    /// </summary>
    /// <param name="requestPublishedMessage">primljena poruka</param>
    private void addRequest(string requestPublishedMessage)
    {
        Console.WriteLine($"--> Adding Request:{requestPublishedMessage}");
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IRequestRecordsRepo>();
            
            var requestPublishedDto = JsonSerializer.Deserialize<RequestPublishedDto>(requestPublishedMessage);
            try
            {
                // TO DO: provjera da li taj Entity već postoji u bazi
                var requestType = repo.GetRequestTypeByName(requestPublishedDto.RequestTypeName);     
                                
                if(requestType != null)
                {
                    Console.WriteLine($"--> RequestType with Key {requestPublishedDto.RequestTypeName} EXISTS...");
                    var request = _mapper.Map<Request>(requestPublishedDto);
                    Console.WriteLine($"--> Request converted! {JsonSerializer.Serialize(request)}");
                    request.DeliveryKey = "email";
                    request.RequestTypeId = requestType.ExternalId;
                    repo.CreateRequest(requestType.ExternalId,
                        new Request {
                        IdentificationKey = request.IdentificationKey,
                        IdentificationString = request.IdentificationString,
                        StartDate = request.StartDate,
                        DeliveryKey  = request.DeliveryKey,
                        RequestTypeId = request.RequestTypeId
                    });
                    repo.SaveChanges();
                    Console.WriteLine($"--> Request added! {JsonSerializer.Serialize(request)}");
                }
                else
                {
                    Console.WriteLine($"--> RequestType with Key {requestPublishedDto.RequestTypeName} NOT exisits...");
                    var request = _mapper.Map<Request>(requestPublishedDto);
                    Console.WriteLine($"--> Request converted! {JsonSerializer.Serialize(request)}");
                    request.DeliveryKey = "email";
                    repo.CreateRequest(requestType.ExternalId,
                        new Request {
                        IdentificationKey = request.IdentificationKey,
                        IdentificationString = request.IdentificationString,
                        StartDate = request.StartDate,
                        DeliveryKey  = request.DeliveryKey,
                        RequestTypeId = request.RequestTypeId
                    });
                    repo.SaveChanges();
                    Console.WriteLine($"--> Request added! {JsonSerializer.Serialize(request)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not add Entity to DB {ex.Message}");
            }
        }
    }
}

enum EventType
{
        EntityPublished,
        SubscriptionPublished,
        RequestPublished,
        Undetermined
}
