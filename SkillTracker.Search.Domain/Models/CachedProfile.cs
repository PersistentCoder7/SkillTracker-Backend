using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Search.Domain.Models
{
    public class CachedProfile
    {
        public string AssociateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<CachedSkillProficiency> Skills { get; set; }
    }

    public class CachedSkillProficiency
    {
        public int SkillId { get; set; }
        public int Proficiency { get; set; }
    }
}
