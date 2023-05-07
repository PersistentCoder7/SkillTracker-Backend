using System;

namespace SkillTracker.Profile.Api.Infrastructure.Exceptions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomErrorMessageAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public CustomErrorMessageAttribute(string errorMessage, int statusCode = 500)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}