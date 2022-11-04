using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Models;
using SkillTracker.Profile.Domain.Commands;
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
                Skills = addProfileDto.Skills
            };
            _bus.SendCommand(addProfileCommand);
        }
    }
}
