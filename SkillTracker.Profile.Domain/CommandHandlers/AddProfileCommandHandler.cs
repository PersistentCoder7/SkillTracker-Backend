using MediatR;
using Microsoft.Extensions.Logging;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Commands;
using SkillTracker.Profile.Domain.Events;

namespace SkillTracker.Profile.Domain.CommandHandlers;

public class AddProfileCommandHandler: IRequestHandler<AddProfileCommand, bool>
{
    private readonly IEventBus _bus;
    private readonly ILogger<AddProfileCommandHandler> _logger;

    public AddProfileCommandHandler(IEventBus bus, ILogger<AddProfileCommandHandler> logger)
    {
        _bus = bus;
        _logger = logger;
    }
    public async Task<bool> Handle(AddProfileCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Publishing  [AddedProfileEvent] for Associate: {request.AssociateId}");
        _bus.Publish<AddedProfileEvent>(new AddedProfileEvent()
        {
            Name = request.Name,
            Email = request.Email,
            AssociateId = request.AssociateId,
            Skills = request.Skills,
            Mobile = request.Mobile
        });
            
        return await Task.FromResult(true);
    }
}