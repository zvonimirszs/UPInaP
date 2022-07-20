using LEX_RequestProcessService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LEX_RequestProcessService.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IdentityServiceAttribute: ActionFilterAttribute
{ 
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //...work with the filterContext object before executing the method
        filterContext.HttpContext.Request.Headers.TryGetValue("SourceKey", out var headerValue);
        if((string)headerValue == null)
        {
            filterContext.Result = new JsonResult(new { message = "SourceKey NE POSTOJI!" }) { StatusCode = StatusCodes.Status451UnavailableForLegalReasons };
            //filterContext.HttpContext.Items["SourceKey"] = headerValue;       
        }
        else
        {
            IRequestProcessRepo repository = (IRequestProcessRepo)filterContext.HttpContext.RequestServices.GetService(typeof(IRequestProcessRepo));
            if(!repository.SourceExists(headerValue))
            {
                filterContext.Result = new JsonResult(new { message = $"SourceKey {headerValue} NIJE DOBAR" }) { StatusCode = StatusCodes.Status451UnavailableForLegalReasons };
            }            
        }
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        //...work with the filterContext object after executing the method
    }
}