using MassTransit;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Infrastructure.Interfaces;
using SkillProficiency = SkillTracker.Profile.Domain.Models.SkillProficiency;

namespace SkillTracker.Profile.Infrastructure.Consumers
{
    public class UpdateProfileConsumer:IConsumer<UpdateProfileMessage>
    {
        private readonly IProfileRepository _repository;
        public UpdateProfileConsumer(IProfileRepository repository)
        {
            _repository = repository;
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
        }
    }
}
