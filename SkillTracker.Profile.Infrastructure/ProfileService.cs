using MassTransit;
using SkillTracker.Common.MessageContracts.Messages;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Domain.Models;
using SkillTracker.Profile.Infrastructure.Interfaces;

namespace SkillTracker.Profile.Infrastructure;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;
    private readonly IPublishEndpoint _bus;

    /*IProfileRepository profileRepository, IEventBus bus*/
    public ProfileService(IProfileRepository repository,IBus bus)
    {
        _repository = repository;
        _bus = bus;
    }
    //public async Task<IEnumerable<Domain.Models.Profile>> GetProfiles()
    //{
    //    return await _profileRepository.GetAllProfiles();
    //}


    public async Task AddProfile(Domain.Models.Profile profile)
    {
        await _bus.Publish<AddProfileMessage>(new AddProfileMessage()
        {
            AssociateId = profile.AssociateId,
            Name = profile.Name,
            Mobile = profile.Mobile,
            Email = profile.Email,
            AddedOn = profile.AddedOn,
            UpdatedOn = profile.UpdatedOn,
            Skills = profile.Skills.Select(s=>new Common.MessageContracts.Messages.SkillProficiency()
            {
                SkillId = s.SkillId,
                Proficiency = s.Proficiency
            }).ToList()
            
        });
        // await _repository.AddProfile(profile);
    }

    public async Task<Domain.Models.Profile> GetProfile(string id)
    {
        return await _repository.GetProfile(id);
    }

    public async Task UpdateProfile(UpdateProfile profile)
    {
        await _bus.Publish<UpdateProfileMessage>(new UpdateProfileMessage()
        {
            AssociateId = profile.AssociateId,
            UpdatedOn = profile.UpdatedOn,
            Skills = profile.Skills.Select(s => new Common.MessageContracts.Messages.SkillProficiency()
            {
                SkillId = s.SkillId,
                Proficiency = s.Proficiency
            }).ToList()

        });
        //await _repository.UpdateProfile(profile);
    }
}