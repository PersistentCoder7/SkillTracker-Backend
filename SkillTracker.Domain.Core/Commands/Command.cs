using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillTracker.Domain.Core.Events;

namespace SkillTracker.Domain.Core.Commands
{
    public abstract  class Command: Message
    {
        public Guid CommandId { get; protected set; }
        public  DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
            CommandId=Guid.NewGuid();
        }
    }
}
