using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Commands;
using SkillTracker.Profile.Domain.Events;

namespace SkillTracker.Profile.Domain.CommandHandlers
{
    public class AddProfileCommandHandler: IRequestHandler<AddProfileCommand, bool>
    {
        private readonly IEventBus _bus;

        public AddProfileCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public async Task<bool> Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {

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
}
