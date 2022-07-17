using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LEX_SubscriptionService.Data;
using LEX_SubscriptionService.Models;
using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using LEX_SubscriptionService.Dtos;
using LEX_SubscriptionService.AsyncDataServices;
using LEX_SubscriptionService.Models.Authenticate;
using LEX_SubscriptionService.SyncDataServices.Http;
using LEX_SubscriptionService.Attributes.Authorization;
using LEX_SubscriptionService.Helpers;
using System.Text.Json;
using LEX_SubscriptionService.Attributes;
using LEX_SubscriptionService.HelpersExceptionMiddleware.Exceptions;

namespace LEX_SubscriptionService.Controllers;

//[Route("api/subscription/{subscriptionId}/[controller]")]
[ApiController]
public class EntityController : ControllerBase
{
    private readonly ISubscriptionRepo _repository;
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBusClient;

    private ICommandDataClient _commandDataClient;

    public EntityController(ISubscriptionRepo repository, IMapper mapper,  ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
    {
        _repository = repository;
        _mapper = mapper;
         _commandDataClient = commandDataClient;
        _messageBusClient = messageBusClient;
    }

    // dohvati sve Entity-e (zahtjeve za pretplatom) po SubscriptionId-u
    [Authorize]
    [IdentityService]
    [Route("api/pretplata/zahtjevi/{subscriptionId}")]    
    [HttpGet]
    public ActionResult<IEnumerable<EntityReadDto>> GetEntitysForSubscription(int subscriptionId)
    {
        Console.WriteLine($"--> Hit GetEntitysForSubscription: {subscriptionId}");

        if (!_repository.SubscriptionExists(subscriptionId))
        {
            //throw new AppException($"NE POSTOJI pretplata pod brojem {subscriptionId} !");
            return NoContent();
        }
        var entitys = _repository.GetEntitysForSubscription(subscriptionId);

        return Ok(_mapper.Map<IEnumerable<EntityReadDto>>(entitys));
    }

    // dohvati Entity (zahtjev za pretplatom) po EntityId-u
    [Authorize]
    [IdentityService]
    [Route("api/pretplata/zahtjev/{subscriptionId}/{entityId}")]
    [HttpGet("{entityId}", Name = "GetEntityForSubscription" )]
    public ActionResult<EntityReadDto> GetEntityForSubscription(int subscriptionId, int entityId)
    {
        Console.WriteLine($"--> Hit GetEntityForSubscription: {subscriptionId} / {entityId}");

        if (!_repository.SubscriptionExists(subscriptionId))
        {
            return NotFound();
        }
        var entity = _repository.GetEntity(subscriptionId, entityId);       
        if(entity == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<EntityReadDto>(entity));
    }

    // kreiraj novi Entity (zahtjev za pretplatom) - uvijeti: SubscriptionId i SourceKey u Headeru
    [Authorize]
    [IdentityService]
    [Route("api/pretplata/zahtjev/{subscriptionId}")]
    [HttpPost]
    public ActionResult<EntityReadDto> CreateEntityForSubscription(int subscriptionId, EntityCreateDto entityCreateDto)
    {
        Console.WriteLine($"--> Hit CreateEntityForSubscription: {subscriptionId}");

        if (!_repository.SubscriptionExists(subscriptionId))
        {
                return NotFound();
        }
        
        Request.Headers.TryGetValue("SourceKey", out var headerValue);
        var entity = _mapper.Map<Entity>(entityCreateDto);
        entity.SubscriptionId = subscriptionId;
        entity.SourceKey = headerValue;

        _repository.CreateEntity(subscriptionId, entity);
        _repository.SaveChanges();

        var entityReadDto = _mapper.Map<EntityReadDto>(entity);
        entityReadDto.SubscriptionName = _repository.GetSubscriptionById(subscriptionId).Name;
        entityReadDto.SubscriptionKey = _repository.GetSubscriptionById(subscriptionId).Key;

        //Send Async Message
        try
        {
            var entityPublishedDto = _mapper.Map<EntityPublishedDto>(entityReadDto);
            entityPublishedDto.Event = "EntitySubscription_Published";
            _messageBusClient.PublishNewEntitySubcription(entityPublishedDto);
        }
        catch (Exception ex)
        {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
        }

        //  return CreatedAtRoute(nameof(GetE),
        //      new {platformId = subscriptionId, commandId = commandReadDto.Id}, commandReadDto);
        //return Ok();
        return RedirectToAction("GetEntityForSubscription",  new {subscriptionId = subscriptionId, entityId = entityReadDto.Id}); 
    }
}
