﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters;

public class ActorValidation:Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var formDate = DateTime.Parse(context.HttpContext.Request.Form["Birthday"]);
        if ((DateTime.Now.Year - formDate.Year < 7 )|| (DateTime.Now.Year - formDate.Year > 99))
        {
            context.Result = new BadRequestResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}
