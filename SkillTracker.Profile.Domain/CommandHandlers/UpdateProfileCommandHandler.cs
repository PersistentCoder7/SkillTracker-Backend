using MediatR;
using Microsoft.Extensions.Logging;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Domain.Commands;
using SkillTracker.Profile.Domain.Events;

namespace SkillTracker.Profile.Domain.CommandHandlers
{
    public class UpdateProfileCommandHandler: IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IEventBus _bus;
        private readonly ILogger<UpdateProfileCommandHandler> _logger;

        public UpdateProfileCommandHandler(IEventBus bus, ILogger<UpdateProfileCommandHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Publishing [UpdatedProfileEvent] for Associate: {request.AssociateId}");
            _bus.Publish<UpdatedProfileEvent>(new UpdatedProfileEvent()
            {
                 AssociateId = request.AssociateId,
                 Skills = request.Skills
            });
           
            return await Task.FromResult(true);
        }
    }
}
