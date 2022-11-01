using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Profile.Domain.Models
{
    public class Profile
    {
        public string AssociateId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
