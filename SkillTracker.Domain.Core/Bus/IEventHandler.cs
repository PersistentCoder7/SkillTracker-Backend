using SkillTracker.Domain.Core.Events;

namespace SkillTracker.Domain.Core.Bus;

public interface IEventHandler<in TEvent>: IEventHandler where TEvent: Event
{
    Task Handle(TEvent @event);
}

public interface IEventHandler
{

}