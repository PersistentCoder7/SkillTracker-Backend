using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Common.MessageContracts.Messages
{
    public class AddProfileMessage
    {
        public string AssociateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<SkillProficiency> Skills { get; set; }
    }

    public class UpdateProfileMessage
    {
        public string AssociateId { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<SkillProficiency> Skills { get; set; }
    }
    public class SkillProficiency
    {
        public int SkillId { get; set; }
        public int Proficiency { get; set; }
    }
}
