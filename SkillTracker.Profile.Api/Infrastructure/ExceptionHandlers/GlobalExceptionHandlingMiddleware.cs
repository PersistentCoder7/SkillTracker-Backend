using System.Net;
using Newtonsoft.Json;
using SkillTracker.Common.Utils.Exceptions;

namespace SkillTracker.Profile.Api.Infrastructure.ExceptionHandlers;

public class GlobalExceptionHandlingMiddleware : IMiddleware
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
        catch (CustomErrorException customError) //Deal with custom errors
        {
            _logger.LogError(customError, customError.Message);
            await WriteToResponseStream(context, customError);
        }
        catch (Exception e) //Convert other general errors into Custom exceptions to make it easier for the client.
        {
            _logger.LogError(e, e.Message);

            var customErrorAttributes = context.GetEndpoint()?.Metadata?.GetOrderedMetadata<CustomErrorMessageAttribute>();
            if (customErrorAttributes != null)
            {
                var lastCustomErrorAttribute = customErrorAttributes[customErrorAttributes.Count - 1];
                var customError = new CustomErrorException(lastCustomErrorAttribute.ErrorMessage,
                    lastCustomErrorAttribute.StatusCode);

                await WriteToResponseStream(context, customError);
            }
            else
            {
                var customError = new CustomErrorException("Server:An unexpected error occurred.",
                    (int)HttpStatusCode.InternalServerError);

                await WriteToResponseStream(context, customError);
            }
        }
    }

    private static async Task WriteToResponseStream(HttpContext context, CustomErrorException customError)
    {
        context.Response.StatusCode = customError.StatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(customError));
    }
}