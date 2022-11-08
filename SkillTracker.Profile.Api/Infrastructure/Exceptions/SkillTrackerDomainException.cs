namespace SkillTracker.Profile.Api.Infrastructure.Exceptions
{
    public class SkillTrackerDomainException: Exception
    {
        public SkillTrackerDomainException()
        {
                
        }

        public SkillTrackerDomainException(string message) : base(message)
        {

        }

        public SkillTrackerDomainException(string message, Exception innException) : base(message, innException)
        {

        }
    }
}
