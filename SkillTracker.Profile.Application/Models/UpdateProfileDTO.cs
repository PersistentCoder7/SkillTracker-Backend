namespace SkillTracker.Profile.Application.Models
{
    public class UpdateProfileDTO
    {
        public string AssociateId { get; set; }
        public List<AddSkillsDTO> Skills { get; set; }
    }
}
