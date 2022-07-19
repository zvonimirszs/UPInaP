using AutoMapper;
using LEX_LegalSettings.Data;
using LEX_LegalSettings.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;

namespace LEX_SubscripLEX_LegalSettingstionService.Controllers;

[ApiController]
public class LegalController : ControllerBase
{
    private readonly ILegalSettingsRepo _repository;
    private readonly IMapper _mapper;

    public LegalController(ILegalSettingsRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
        
    [Route("api/postavke/autorizacija")]
    [HttpPost]
    public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
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
}