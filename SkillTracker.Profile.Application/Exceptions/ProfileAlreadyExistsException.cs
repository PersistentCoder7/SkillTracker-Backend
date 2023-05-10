using System.Runtime.Serialization;

namespace SkillTracker.Profile.Application.Exceptions
{
    public class ProfileAlreadyExistsException : Exception
    {
        public ProfileAlreadyExistsException()
        {
        }

        public ProfileAlreadyExistsException(string? message) : base(message)
        {
        }

        public ProfileAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ProfileAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
