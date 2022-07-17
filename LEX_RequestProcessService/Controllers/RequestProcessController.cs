using System.Text.Json;
using AutoMapper;
using LEX_RequestProcessService.AsyncDataServices;
using LEX_RequestProcessService.Attributes;
using LEX_RequestProcessService.Attributes.Authorization;
using LEX_RequestProcessService.Data;
using LEX_RequestProcessService.Dtos;
using LEX_RequestProcessService.Models;
using LEX_RequestProcessService.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace LEX_RequestProcessService.Controllers;

//[Route("api/responsetype/{responsetypeId}/[controller]")]
[ApiController]
public class RequestProcessController : ControllerBase
{
    private readonly IRequestProcessRepo _repository;
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBusClient;

    public RequestProcessController(IRequestProcessRepo repository, IMapper mapper,  IMessageBusClient messageBusClient)
    {
        _repository = repository;
        _mapper = mapper;
        _messageBusClient = messageBusClient;
    }

    [AllowAnonymous]
    [Route("api/pravonapristup/autorizacija")]
    [HttpPost]
    public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        AuthenticateResponse authenticateResponse = _repository.Authenticate(model) ;
        if(authenticateResponse == null)
        {
            return Unauthorized(model);
        }
        else
        {
            return(authenticateResponse);
        }
    }



    //TO DO: authorizacija 
    [Authorize]
    [Route("api/pravonapristup/odgovor/{id}")]
    //[HttpPost("{id}", Name = "GetResponse")]
    public ActionResult<IEnumerable<EntityReadDto>> GetResponse(int id)
    {
        Console.WriteLine("--> Getting Response Process...");
        // dohvati aktivan request
        var requestItem = _repository.GetRequestById(id);
        if (requestItem == null)
        {
            Console.WriteLine($"--> NOT FOUND requestItem {id}...");
            return NotFound();
        }

        var requestType = _repository.GetResponseTypeById(requestItem.ResponseTypeId);
        // dohvati entity prema parametrima preteživanja u requestu 
        var entityItems = _repository.GetEntityByKey(requestItem.IdentificationKey, requestItem.IdentificationString, requestType);

        if (entityItems == null)
        {
            Console.WriteLine("--> NOT FOUND entityItems...");
            return NotFound();
        }
        if (entityItems.Count() == 0)
        {
            Console.WriteLine("--> EMPTY entityItems...");
            return NotFound();
        }
        var ProcessInfoItems = _repository.GetProcessInfoForEntitys(entityItems);
        // prebaci u EntityRead objekt + spoji sa Subscription objektom
        var entitysReadDto = (from e in entityItems
            select new EntityReadDto
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Address = e.Address,
                City = e.City,  
                PostNo = e.PostNo,
                Description = e.Description,
                ProcessInfo =  ProcessInfoItems.Where(p => p.EntityId == e.Id).FirstOrDefault(),           
                Subscription = _repository.GetSubscriptionById(e.SubscriptionId)
            }).ToList();

        Console.WriteLine($"--> entitysReadDto {entitysReadDto.Count()}...");
        return Ok(entitysReadDto);
    }
    [Authorize]
    [IdentityService]
    [Route("api/pravonapristup/pretplate")]
    public ActionResult<Subscription> GetSubscriptions()
    {
        Console.WriteLine($"--> Getting Subscriptions ...");

        var subscriptionItems = _repository.GetSubscriptions();
        if (subscriptionItems != null)
        {
            //return Ok(subscriptionItems);
            return Ok(_mapper.Map<IEnumerable<SubscriptionReadDto>>(subscriptionItems));
        }

        return NotFound();
    }
    [AllowAnonymous]
    [IdentityService]
    [Route("api/pravonapristup/zahtjevi")]
    public ActionResult<Entity> GetEntitys()
    {
        Console.WriteLine($"--> Getting Entitys ...");

        var entityItems = _repository.GetAllEntitys();
        if (entityItems != null)
        {
            //return Ok(entityItems);
            return Ok(_mapper.Map<IEnumerable<EntityReadDto>>(entityItems));
        }

        return NotFound();
    }

    [Authorize] 
    [IdentityService]
    [Route("api/pravonapristup/upiti")]
    [HttpGet]
    public ActionResult<IEnumerable<Request>> GetRequests()
    {
        Console.WriteLine("--> Getting Requests Process...");

        var requestItem = _repository.GetAllRequests();
     
        return Ok(_mapper.Map<IEnumerable<RequestReadDto>>(requestItem));
    }  

    [Authorize]
    [IdentityService]
    [Route("api/pravonapristup/upit")]
    [HttpPost]
    public ActionResult<RequestReadDto> CreateRequest(RequestCreateDto requestCreateDto)
    {
        Console.WriteLine($"--> Hit CreateRequest Process");
        // dohvaćanje SourceKey iz Headera
        Request.Headers.TryGetValue("SourceKey", out var headerValue);
        // mapiranje objakta RequestCreateDto u Request
        var request = _mapper.Map<Request>(requestCreateDto);
        //dodavanje SourceKey u Request
        request.SourceKey = headerValue;
        //kreiranjeRequest u DB
        _repository.CreateRequest(request);
        _repository.SaveChanges();
        // mapiranje objakta Request u RequestReadDto
        var requestReadDto = _mapper.Map<RequestReadDto>(request);
        Console.WriteLine($"--> requestReadDto: {requestReadDto.Id}");
        //slanje Async poruke (MQ) - da je zahtjev zaprimljen i kreiran  
        try
        {
            // mapiranje objakta RequestReadDto u RequestPublishedDto
            var requestPublishedDto = _mapper.Map<RequestPublishedDto>(requestReadDto);
            // definiranje Eventa
            requestPublishedDto.Event = "RequestProcess_Published";
            // slanje poruke u MQ 
            _messageBusClient.PublishNewRequestProcess(requestPublishedDto);
        }
        catch (Exception ex)
        {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
        }
        Console.WriteLine($"--> requestReadDto: {requestReadDto.Id}");

        return RedirectToAction("GetResponse", new { Id = requestReadDto.Id }); 
    } 
    [Route("api/pravonapristup/servisnaobavijest")]
    [HttpPost]
    public ActionResult ServicePost(IEnumerable<Source> sourceItems)
    {
        Console.WriteLine($"-->Inbound POST # Service. {sourceItems.Count()}");

        //return Ok($"Inbound POST test of from LEX_RequestProcess Controller. Recive {sourceItems.Count()} izvora.");
        //_mapper.Map<IEnumerable<EntityReadDto>>(entitys)
        return Ok(JsonSerializer.Serialize(sourceItems));
    }
}