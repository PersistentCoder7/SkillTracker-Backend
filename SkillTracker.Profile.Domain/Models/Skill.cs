using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Profile.Domain.Models
{
    public class Skill
    {
        public bool IsTechnical { get; set; }

        //[Required(ErrorMessage = "Skill Name is required")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Proficiency is required")]
        //[Range(1, 20, ErrorMessage = "Invalid Proficiency")]
        public int Proficiency { get; set; }
    }

}
