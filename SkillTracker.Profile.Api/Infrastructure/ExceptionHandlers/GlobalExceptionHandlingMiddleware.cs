using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SkillTracker.Profile.Api.Infrastructure.Exceptions;

namespace SkillTracker.Profile.Api.Infrastructure.ExceptionHandlers
{
    public class GlobalExceptionHandlingMiddleware: IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await SendResponse(context, e);
            }
        }

        private async Task SendResponse(HttpContext context, Exception e)
        {
            CustomError customError = new(e.Message,e.Data.ToString(),context.Response.StatusCode);
            string json = JsonConvert.SerializeObject(customError);
            context.Response.ContentType="application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
