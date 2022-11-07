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
    public class UpdateProfileCommandHandler: IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IEventBus _bus;

        public UpdateProfileCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {

            _bus.Publish<UpdatedProfileEvent>(new UpdatedProfileEvent()
            {
                 AssociateId = request.AssociateId,
                 Skills = request.Skills
            });
            return await Task.FromResult(true);
        }
    }
}
