using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Models
{
    public class AddProfileDTO
    {
        public string AssociateId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
