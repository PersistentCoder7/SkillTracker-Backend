using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SkillTracker.Profile.Api.Infrastructure.Exceptions;
using System.Net;

namespace SkillTracker.Profile.Api.ActionFilters
{
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
                var customError = new CustomError("Validation failed", errorMessage, (int)HttpStatusCode.BadRequest);
                context.Result = new BadRequestObjectResult(customError);
            }
        }
    }

}
