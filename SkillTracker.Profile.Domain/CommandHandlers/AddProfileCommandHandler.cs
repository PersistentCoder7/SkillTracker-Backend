using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Commands;

namespace SkillTracker.Profile.Domain.CommandHandlers
{
    public class AddProfileCommandHandler: IRequestHandler<AddProfileCommand, bool>
    {
        private readonly IEventBus _bus;

        public AddProfileCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task<bool> Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {

            return Task.FromResult(true);
        }
    }
}
