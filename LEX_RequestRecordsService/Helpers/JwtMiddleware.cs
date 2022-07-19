namespace LEX_RequestRecordsService.Helpers;

using AutoMapper;
using LEX_RequestRecordsService.Data;
using LEX_RequestRecordsService.Models.Authenticate;
using Microsoft.Extensions.Options;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMapper _mapper;

    public JwtMiddleware(RequestDelegate next, IMapper mapper)
    {
         Console.WriteLine($"--> Calling JwtMiddleware");
        _next = next;
        _mapper = mapper;
    }

    public Task Invoke (HttpContext context, IRequestRecordsRepo repository)
    {
        Console.WriteLine($"--> Calling Invoke in JwtMiddleware");
        string authHeader = context.Request.Headers["Authorization"];  
        if(authHeader != null && authHeader.StartsWith("Bearer"))
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(token != null)
            {
                var response = repository.ValidateToken(token);
                if (response != null)
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = _mapper.Map<User>(response);
                }
            }
        } 
        return _next(context);
    }
}