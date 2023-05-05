namespace SkillTracker.Profile.Api.Infrastructure.Exceptions
{
    public class CustomError
    {
        public string Message { get; private set; }
        public string Details { get; private set; }
        public int StatusCode { get; private set; }

        public CustomError(string message, string details, int statusCode)
        {
            Message = message;
            Details = details;
            StatusCode = statusCode;
        }
    }

}
