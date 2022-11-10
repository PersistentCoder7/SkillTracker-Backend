using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Events;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Domain.EventHandlers
{
    public  class AddedProfileEventHandler: IEventHandler<AddedProfileEvent>
    {
        private readonly IProfileRepository _repository;
        private readonly ILogger<AddedProfileEventHandler> _logger;

        public AddedProfileEventHandler(IProfileRepository repository, ILogger<AddedProfileEventHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Task Handle(AddedProfileEvent @event)
        {
            var profile = new Models.Profile()
                {
                    AssociateId = @event.AssociateId,
                    Email = @event.Email,
                    Mobile = @event.Mobile,
                    Name = @event.Name,
                    Skills = @event.Skills,
                    AddedOn = @event.AddedOn,
                    UpdatedOn = @event.UpdatedOn
                    //UserId = Guid.NewGuid().ToString()
                    
                }
;
             _repository.SaveProfile(profile);
             _logger.LogInformation($"[AddProfileEvent] is processed for Associate: {@event.AssociateId}");
            return Task.CompletedTask;
        }
    }
}
