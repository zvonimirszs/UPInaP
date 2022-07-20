using System.Text.Json;
using AutoMapper;
using LEX_RequestProcessService.Data;
using LEX_RequestProcessService.Dtos;
using LEX_RequestProcessService.Models;


namespace LEX_RequestProcessService.EventProcessing;
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
            case EventType.EntityPublished:
                addEntity(message);
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
            default:
                Console.WriteLine("--> Could not determine the event type");
                return EventType.Undetermined;
        }
    }
    /// <summary>
    /// Dodavanje Entity-a koji se pretplatio
    /// </summary>
    /// <param name="entityPublishedMessage">primljena poruka</param>
    private void addEntity(string entityPublishedMessage)
    {
        Console.WriteLine($"--> Adding Entity:{entityPublishedMessage}");
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IRequestProcessRepo>();
            
            var entityPublishedDto = JsonSerializer.Deserialize<EntityPublishedDto>(entityPublishedMessage);
            try
            {
                // TO DO: provjera da li taj Entity već postoji u bazi
                var subscription = repo.GetSubscriptionByKey(entityPublishedDto.SubscriptionKey);     
                                
                if(subscription != null)
                {
                    //Console.WriteLine($"--> Subscription with Key {subscription.Key} EXISTS...");
                    var entity = _mapper.Map<Entity>(entityPublishedDto);
                    //Console.WriteLine($"--> Entity converted! {JsonSerializer.Serialize(entity)}");
                    entity.SubscriptionId = subscription.ExternalId;
                    entity.Connected = true;
                    repo.CreateEntity(new Entity {
                        ExternalId = entity.ExternalId,
                        SourceKey = entity.SourceKey,
                        SubscriptionId = entity.SubscriptionId,
                        Connected = entity.Connected,
                        Email = entity.Email
                    });                    
                    repo.SaveChanges();
                    Console.WriteLine($"--> Entity added! {JsonSerializer.Serialize(entity)}");
                }
                else
                {
                    Console.WriteLine($"--> Subscription with Key {entityPublishedDto.SubscriptionKey} NOT exisits...");
                    var entity = _mapper.Map<Entity>(entityPublishedDto);
                    Console.WriteLine($"--> Entity converted! {JsonSerializer.Serialize(entity)}");
                    entity.Connected = false;
                    repo.CreateEntity(new Entity {
                        ExternalId = entity.ExternalId,
                        SourceKey = entity.SourceKey,
                        Connected = entity.Connected,
                        Email = entity.Email
                    });
                    repo.SaveChanges();
                    Console.WriteLine($"--> Entity added! {JsonSerializer.Serialize(entity)}");
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
        Undetermined
}
