using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Application.Helpers;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Models;
using SkillTracker.Profile.Domain.Commands;
using SkillTracker.Profile.Domain.Events;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Application.Services
{
    public  class ProfileService: IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IEventBus _bus;

        public ProfileService(IProfileRepository profileRepository, IEventBus bus)
        {
            _profileRepository = profileRepository;
            _bus = bus;
        }
        public async Task<IEnumerable<Domain.Models.Profile>> GetProfiles()
        {
            return await _profileRepository.GetAllProfiles();
        }

        public void AddProfile(AddProfileDTO addProfileDto)
        {
            //To Add the data from dto to Command
            var addProfileCommand = new AddProfileCommand()
            {
                AssociateId = addProfileDto.AssociateId,
                Email = addProfileDto.Email,
                Name = addProfileDto.Name,
                Mobile = addProfileDto.Mobile,
                Skills = addProfileDto.Skills.GetSkills(associateId:addProfileDto.AssociateId)
            };
            _bus.SendCommand(addProfileCommand);
        }

        public async Task<Domain.Models.Profile> GetProfile(string id)
        {
           return await _profileRepository.GetProfile(id);
        }

        public void UpdateProfile(UpdateProfileDTO updateProfileDto)
        {
            var updateProfileCommand = new UpdateProfileCommand()
            {
                AssociateId = updateProfileDto.AssociateId,
                Skills = updateProfileDto.Skills.GetSkills(updateProfileDto.AssociateId)
            };
            _bus.SendCommand(updateProfileCommand);
        }

        public async Task<List<Domain.Models.Profile>> Search(SearchProfileDTO searchProfileDto)
        {
            return await _profileRepository.Search(new SearchProfileEvent()
            {
                AssociateId = searchProfileDto.AssociateId,
                Name = searchProfileDto.Name,
                Skill = searchProfileDto.Skill
            });
        }
    }
}
