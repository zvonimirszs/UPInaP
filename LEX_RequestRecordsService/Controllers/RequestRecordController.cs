using AutoMapper;
using LEX_RequestRecordsService.AsyncDataServices;
using LEX_RequestRecordsService.Attributes.Authorization;
using LEX_RequestRecordsService.Data;
using LEX_RequestRecordsService.Dtos;
using LEX_RequestRecordsService.Models;
using LEX_RequestRecordsService.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LEX_RequestRecordsService.Controllers;

//[Route("api/requesttype/{requesttypeId}/[controller]")]
[ApiController]
public class RequestRecordsController : ControllerBase
{
    private readonly IRequestRecordsRepo _repository;
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBusClient;

    public RequestRecordsController(IRequestRecordsRepo repository, IMapper mapper,  IMessageBusClient messageBusClient)
    {
        _repository = repository;
        _mapper = mapper;
        _messageBusClient = messageBusClient;
    }
    
    [AllowAnonymous]
    [Route("api/pravaispitanika/autorizacija")]
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

    [Authorize]
    [Route("api/pravaispitanika/zahtjevi")]
    [HttpGet]
    public ActionResult<IEnumerable<Request>> GetRequests()
    {
        Console.WriteLine("--> Getting Requests Records...");

        var requestItem = _repository.GetAllRequests();
     
        return Ok(_mapper.Map<IEnumerable<RequestReadDto>>(requestItem));
    }  

    [Authorize]
    [Route("api/pravaispitanika/zahtjev/{requestId}")]
    //[HttpGet]
    public ActionResult<Request> GetRequestByRequestId(int requestId)
    {
        Console.WriteLine($"--> Getting Requests Records By Reguest Id: {requestId}...");

        var requestItem = _repository.GetRequestById(null, requestId);
     
        return Ok(_mapper.Map<RequestReadDto>(requestItem));
    }  

    [Authorize]
    [Route("api/pravaispitanika/zahtjevi/{requesttypeId}")]
    [HttpGet]
    public ActionResult<IEnumerable<Request>> GetRequestsByRequestType(int requestTypeId)
    {
        Console.WriteLine($"--> Getting Requests Records By ReguestType Id: {requestTypeId}...");

        var requestItem = _repository.GetRequestByRequestTypeId(requestTypeId);
     
        return Ok(_mapper.Map<IEnumerable<RequestReadDto>>(requestItem));
    }  

    [Authorize]
    [Route("api/pravaispitanika/zahtjev/{requesttypeId}")]
    [HttpPost]
    public ActionResult<RequestReadDto> CreateRequestForType(int requestTypeId, RequestCreateDto requestDto)
    {
        Console.WriteLine($"--> Hit CreateRequestForType: {requestTypeId}");

        if (!_repository.RequestTypeExists(requestTypeId))
        {
            return NotFound();
        }

        var request = _mapper.Map<Request>(requestDto);
        //TO DO: što kada nema SourceKey u headeru?
        Request.Headers.TryGetValue("SourceKey", out var headerValue);
        request.StartDate = DateTime.Now;  

        _repository.CreateRequest(requestTypeId, request);
        _repository.SaveChanges();

        request.RequestType = _repository.GetAllRequestTypes().FirstOrDefault(p => p.Id == requestTypeId);
        var requestReadDto = _mapper.Map<RequestReadDto>(request);

        //Send Async Message
        try
        {
            var requestPublishedDto = _mapper.Map<RequestPublishedDto>(requestReadDto);
            requestPublishedDto.Event = "RequestRecord_Published";
            requestPublishedDto.SourceKey = headerValue;
            _messageBusClient.PublishNewRequestRecord(requestPublishedDto);
        }
        catch (Exception ex)
        {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
        }

        // return CreatedAtRoute(nameof(GetRequest),
        //     new {requestId = requestReadDto.Id}, requestReadDto);
        return RedirectToAction("GetRequestByRequestId", new { requestId = requestReadDto.Id }); 
        //return Ok();
    }
}
