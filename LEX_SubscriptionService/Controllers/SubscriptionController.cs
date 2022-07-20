using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using LEX_SubscriptionService.Data;
using LEX_SubscriptionService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LEX_SubscriptionService.Dtos;
using LEX_SubscriptionService.AsyncDataServices;
using LEX_SubscriptionService.Attributes.Authorization;
using LEX_SubscriptionService.Models.Authenticate;
using LEX_SubscriptionService.Helpers;
using System.Text.Json;
using LEX_SubscriptionService.Attributes;
using LEX_SubscriptionService.SyncDataServices.Http;

namespace LEX_SubscriptionService.Controllers;

[ApiController]
public class SubscriptionController : ControllerBase
{
    private ICommandDataClient _commandDataClient;
    private readonly ISubscriptionRepo _repository;
    private readonly IMapper _mapper;

    public SubscriptionController(ISubscriptionRepo repository, IMapper mapper,ICommandDataClient commandDataClient)
    {
        _repository = repository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
    }
        
    [AllowAnonymous]
    [Route("api/pretplata/autorizacija")]
    [HttpPost]
    public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        Console.WriteLine("--> Getting Authenticate...");
        AuthenticateResponse authenticateResponse = _repository.Authenticate(model) ;
        if(authenticateResponse == null)
        {
            //throw new AppException("Korisničko ime i lozinka: NISU DOBRI!");
            return Unauthorized("Korisničko ime i lozinka: NISU DOBRI!");
        }
        else
        {
            return(authenticateResponse);
        }
    }

    [Authorize]
    [IdentityService]
    [Route("api/pretplata")]
    [HttpGet]
    public ActionResult<IEnumerable<Subscription>> GetSubscriptions()
    {
        Console.WriteLine("--> Getting Subscriptions...");

        var subscriptionItem = _repository.GetAllSubscriptions();
     
        return Ok(_mapper.Map<IEnumerable<SubscriptionReadDto>>(subscriptionItem));
    }  

    [Authorize]
    [IdentityService]
    [Route("api/pretplata/izvori")]
    [HttpPost]
    public async Task<ActionResult> GetSources()
    {
        Console.WriteLine("--> Getting Sources...");

        // Send Sync Message
        try
        {
            string authHeader = Request.Headers["Authorization"];  
            if(authHeader != null && authHeader.StartsWith("Bearer"))
            {
                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if(token != null)
                {
                    var sourceItem = _repository.GetAllSource();
                    HttpResponseMessage response =  await _commandDataClient.SendSourcesToRequestProcess(token, sourceItem);  
                    
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Console.WriteLine($"--> Could not send synchronously: TOKEN Bearer NOT EXISTS"); 
                    return Ok($"--> Could not send synchronously: TOKEN Bearer NOT EXISTS");
                }
            }
            else
            {
                Console.WriteLine($"--> Could not send synchronously: TOKEN NOT EXISTS"); 
                return Ok($"--> Could not send synchronously: TOKEN NOT EXISTS");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously: {ex.InnerException.StackTrace}, {ex.InnerException.Message}");
            return Ok($"--> Could not send synchronously: {ex.InnerException.StackTrace}, {ex.InnerException.Message}");
        }
     
        //return Ok();
    } 
}
