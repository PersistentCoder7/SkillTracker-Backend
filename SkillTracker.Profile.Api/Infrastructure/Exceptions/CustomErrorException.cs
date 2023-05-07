using System;

namespace SkillTracker.Profile.Api.Infrastructure.Exceptions
{
    public class CustomErrorException : Exception
    {
        public int StatusCode { get; set; }

        public CustomErrorException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomErrorException(string message, Exception innerException, int statusCode = 500) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}