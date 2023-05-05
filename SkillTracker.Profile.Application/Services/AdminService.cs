using SkillTracker.Domain.Core.Bus;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Models;
using SkillTracker.Profile.Domain.Events;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Profile.Application.Services
{
    public  class AdminService: IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IEventBus _bus;

        public AdminService(IAdminRepository adminRepository, IEventBus bus)
        {
            _adminRepository = adminRepository;
            _bus = bus;
        }

        public async Task<List<Domain.Models.Profile>> Search(SearchProfileDTO searchProfileDto)
        {
            return await _adminRepository.Search(new SearchProfileEvent()
            {
                AssociateId = searchProfileDto.AssociateId,
                Name = searchProfileDto.Name,
                Skill = searchProfileDto.Skill
            });
        }
    }
}
