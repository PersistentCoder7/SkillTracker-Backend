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

        public UpdateProfileConsumer(IProfileRepository repository,IBus bus)
        {
            _repository = repository;
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<UpdateProfileMessage> context)
        {
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
