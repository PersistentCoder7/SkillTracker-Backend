using Microsoft.AspNetCore.Mvc.Filters;
using SkillTracker.Profile.Api.Infrastructure.Exceptions;
using System.Net;

namespace SkillTracker.Profile.Api.ActionFilters;

public class ValidateDtoAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            var errorMessage = string.Join(" ", errors);
            throw new CustomErrorException(errorMessage, (int)HttpStatusCode.BadRequest);
        }
    }
}