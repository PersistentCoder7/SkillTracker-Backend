using Microsoft.Extensions.Logging;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Events;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Domain.EventHandlers;

public  class UpdatedProfileEventHandler: IEventHandler<UpdatedProfileEvent>
{
    private readonly IProfileRepository _repository;
    private readonly ILogger<UpdatedProfileEventHandler> _logger;

    public UpdatedProfileEventHandler(IProfileRepository repository, ILogger<UpdatedProfileEventHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task Handle(UpdatedProfileEvent @event)
    {
        //Get the profile
        var profile =  await _repository.GetProfile(@event.AssociateId);

        //Update the profile
        profile.Skills=@event.Skills;
        profile.UpdatedOn=DateTime.Now;

        await _repository.SaveProfile(profile);
        _logger.LogInformation($"[UpdatedProfileEvent] is processed for Associate: {@event.AssociateId}");
        //return Task.CompletedTask;
    }
}