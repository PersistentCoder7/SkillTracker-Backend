using MassTransit;
using Microsoft.Extensions.Logging;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Infrastructure.Interfaces;
using SkillProficiency = SkillTracker.Profile.Domain.Models.SkillProficiency;

namespace SkillTracker.Profile.Infrastructure.Consumers
{
    public class UpdateProfileConsumer:IConsumer<UpdateProfileMessage>
    {
        private readonly IProfileRepository _repository;
        private readonly IBus _bus;
        private readonly ILogger<UpdateProfileConsumer> _logger;

        public UpdateProfileConsumer(IProfileRepository repository,IBus bus,ILogger<UpdateProfileConsumer> logger)
        {
            _repository = repository;
            _bus = bus;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<UpdateProfileMessage> context)
        {
            _logger.LogInformation($"Updating profile {context.Message.AssociateId} to database");
            await _repository.UpdateProfile(new Domain.Models.UpdateProfile()
            {
                AssociateId = context.Message.AssociateId,
                UpdatedOn = context.Message.UpdatedOn,
                Skills = context.Message.Skills.Select(x =>
                    new SkillProficiency()
                    {
                        SkillId = x.SkillId,
                        Proficiency = x.Proficiency
                    }).ToList()
            });

            await _bus.Publish<RefreshCacheEvent>(new RefreshCacheEvent());
        }
    }
}
