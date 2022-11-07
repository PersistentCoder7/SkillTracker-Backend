using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Profile.Application.Models
{
    public class UpdateProfileDTO
    {
        public string AssociateId { get; set; }
        public List<AddSkillsDTO> Skills { get; set; }
    }
}
