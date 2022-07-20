namespace LEX_RequestProcessService.Attributes.Authorization;

using LEX_RequestProcessService.Models.Authenticate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine($"--> Calling OnAuthorization in AuthorizeAttribute");
        // preskoči autorizaciju ako je akcija dekorirana sa [AllowAnonymous] atributom
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // autorizacija
        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}