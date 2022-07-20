using AutoMapper;
using LEX_RequestRecordsService.Attributes.Authorization;
using LEX_RequestRecordsService.Data;
using LEX_RequestRecordsService.Dtos;
using LEX_RequestRecordsService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LEX_RequestRecordsService.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class RequestTypeController : ControllerBase
{
    private readonly IRequestRecordsRepo _repository;
    private readonly IMapper _mapper;

    public RequestTypeController(IRequestRecordsRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [Route("api/pravaispitanika/tipovi")]
    [HttpGet]
    public ActionResult<IEnumerable<Request>> GetRequestTypes()
    {
        Console.WriteLine("--> Getting RequestTypes...");

        var requestTypeItem = _repository.GetAllRequestTypes();
     
        return Ok(_mapper.Map<IEnumerable<RequestTypeReadDto>>(requestTypeItem));
    }   
}
