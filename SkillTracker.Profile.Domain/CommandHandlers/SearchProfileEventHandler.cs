using MediatR;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Commands;
using SkillTracker.Profile.Domain.Events;

namespace SkillTracker.Profile.Domain.CommandHandlers;

public class SearchProfileCommandHandler: IRequestHandler<SearchProfileCommand, bool>
{
    private readonly IEventBus _bus;

    public SearchProfileCommandHandler(IEventBus bus)
    {
        _bus = bus;
    }
    public async Task<bool> Handle(SearchProfileCommand request, CancellationToken cancellationToken)
    {

        _bus.Publish<SearchProfileEvent>(new SearchProfileEvent()
        {
            AssociateId = request.AssociateId,
            Name = request.Name,
            Skill = request.Skill
        });
        return await Task.FromResult(true);
    }
}