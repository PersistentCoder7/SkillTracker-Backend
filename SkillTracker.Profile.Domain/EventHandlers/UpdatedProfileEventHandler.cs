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
    public  class UpdatedProfileEventHandler: IEventHandler<UpdatedProfileEvent>
    {
        private readonly IProfileRepository _repository;

        public UpdatedProfileEventHandler(IProfileRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdatedProfileEvent @event)
        {
            //Get the profile
            var profile =  await _repository.GetProfile(@event.AssociateId);

            //Update the profile
            profile.Skills=@event.Skills;
            profile.UpdatedOn=DateTime.Now;

            await _repository.SaveProfile(profile);
            //return Task.CompletedTask;
        }
    }
}
