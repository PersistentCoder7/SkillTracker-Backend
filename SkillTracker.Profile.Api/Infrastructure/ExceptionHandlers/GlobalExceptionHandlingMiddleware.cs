using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using SkillTracker.Common.Utils.Exceptions;
using SkillTracker.Profile.Application.Exceptions;

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
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var httpStatusCode = (int)HttpStatusCode.InternalServerError;
        var content = "Server:An unexpected error occurred.";

        switch (exception)
        {
            case CustomValidationException e:
                content = e.Message;
                break;
            case NotFoundException e:
                httpStatusCode = (int)HttpStatusCode.NotFound;
                content = e.Message;
                break;
            case ProfileAlreadyExistsException e:
                httpStatusCode = (int)HttpStatusCode.Conflict;
                content = e.Message;
                break;
            default:
                var customErrorAttributes = context.GetEndpoint()?.Metadata?.GetOrderedMetadata<CustomErrorMessageAttribute>();
                if (customErrorAttributes != null)
                {
                    var lastCustomErrorAttribute = customErrorAttributes[customErrorAttributes.Count - 1];
                    content = lastCustomErrorAttribute.ErrorMessage;
                    httpStatusCode = lastCustomErrorAttribute.StatusCode;
                }
                break;
        }

        var customException = new CustomErrorException(content, httpStatusCode);
        await WriteToResponseStream(context, customException);
    }

    private static async Task WriteToResponseStream(HttpContext context, CustomErrorException customError)
    {
        context.Response.StatusCode = customError.StatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(customError));
    }
}