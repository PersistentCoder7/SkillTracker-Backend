using MassTransit;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Infrastructure.Interfaces;
using SkillProficiency = SkillTracker.Profile.Domain.Models.SkillProficiency;

namespace SkillTracker.Profile.Infrastructure.Consumers
{
    public class AddProfileConsumer:IConsumer<AddProfileMessage>
    {
        private readonly IProfileRepository _repository;
        public AddProfileConsumer(IProfileRepository repository)
        {
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<AddProfileMessage> context)
        {
            await _repository.AddProfile(new Domain.Models.Profile()
            {
                Name = context.Message.Name,
                Mobile = context.Message.Mobile,
                AddedOn = context.Message.AddedOn,
                AssociateId = context.Message.AssociateId,
                Email = context.Message.Email,
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
