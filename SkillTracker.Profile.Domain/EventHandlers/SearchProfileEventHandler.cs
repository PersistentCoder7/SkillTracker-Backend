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
    public  class SearchProfileEventHandler: IEventHandler<SearchProfileEvent>
    {
        private readonly IProfileRepository _repository;

        public SearchProfileEventHandler(IProfileRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(SearchProfileEvent @event)
        {
            await _repository.Search(@event);
            //return Task.CompletedTask;
        }
    }
}
