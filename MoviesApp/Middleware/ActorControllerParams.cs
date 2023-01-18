using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Middleware;

public class ActorControllerParams
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ActorControllerParams> _logger;

    public ActorControllerParams(RequestDelegate next, ILogger<ActorControllerParams> logger)
    {
        _next = next;
        _logger = logger;
    }

    public Task Invoke(HttpContext httpContext, ILogger<ActorControllerParams> logger)
    {
        if (httpContext.Request.GetDisplayUrl().Contains("Actor"))
        {
            logger.LogInformation($"Request: {httpContext.Request.Path} /{httpContext.Request.Protocol} / {httpContext.Request.ContentType} / {httpContext.Request.ReadFormAsync()}  Method: {httpContext.Request.Method}");
            logger.LogTrace($"Request:  Method: {httpContext.Request.Method}");
            
        }

        return _next(httpContext);
    }

    }
