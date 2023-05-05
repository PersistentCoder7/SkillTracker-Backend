namespace SkillTracker.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime TimeStamp { get; protected set; }
        public Guid EventId { get; protected set; }
        protected Event()
        {
            TimeStamp = DateTime.Now;
            EventId = Guid.NewGuid();
        }
    }
}
