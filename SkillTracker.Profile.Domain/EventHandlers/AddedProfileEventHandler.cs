using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Events;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Domain.EventHandlers
{
    public  class AddedProfileEventHandler: IEventHandler<AddedProfileEvent>
    {
        private readonly IProfileRepository _repository;

        public AddedProfileEventHandler(IProfileRepository repository)
        {
            _repository = repository;
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
                }
;
             _repository.SaveProfile(profile);
            return Task.CompletedTask;
        }
    }
}
