using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Domain.Core.Events;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Domain.Events
{
    public class UpdatedProfileEvent: Event
    {
        
        public string AssociateId { get; set; }

     
        public DateTime? UpdatedOn { get; set; }

        public List<Skill> Skills { get; set; }

        public UpdatedProfileEvent()
        {
            UpdatedOn = DateTime.Now;
        }
    }
}
