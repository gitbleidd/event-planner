﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EventPlanner.App.Filters;

public class DatabaseExceptionFilter : Attribute, IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    public DatabaseExceptionFilter(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DbUpdateException exc && exc.InnerException is PostgresException pgException)
        {
            if (_hostEnvironment.IsDevelopment())
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Content = $"{exc.Message}\n{pgException.Detail}" ,
                };
            }
            else
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                };
            }
            
            context.ExceptionHandled = true;
        }
    }
} 