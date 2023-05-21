using MassTransit;
using Microsoft.Extensions.Logging;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Infrastructure.Interfaces;
using SkillProficiency = SkillTracker.Profile.Domain.Models.SkillProficiency;

namespace SkillTracker.Profile.Infrastructure.Consumers
{
    public class AddProfileConsumer:IConsumer<AddProfileMessage>
    {
        private readonly IProfileRepository _repository;
        private readonly IBus _bus;
        private readonly ILogger<AddProfileConsumer> _logger;

        public AddProfileConsumer(IProfileRepository repository,IBus bus,ILogger<AddProfileConsumer> logger)
        {
            _repository = repository;
            _bus = bus;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<AddProfileMessage> context)
        {
            _logger.LogInformation($"Adding profile {context.Message.AssociateId} to database");
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
            await _bus.Publish<RefreshCacheEvent>(new RefreshCacheEvent());
        }
    }
}
