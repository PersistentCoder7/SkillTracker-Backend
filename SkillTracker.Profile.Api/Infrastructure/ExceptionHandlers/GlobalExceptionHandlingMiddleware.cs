using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            ProblemDetails problem = new()
            {
                Status = context.Response.StatusCode,
                Type = e.GetType().Name,
                Title = e.GetType().Name,
                Detail = e.Message

            };
            string json = JsonConvert.SerializeObject(problem);
            context.Response.ContentType="application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
