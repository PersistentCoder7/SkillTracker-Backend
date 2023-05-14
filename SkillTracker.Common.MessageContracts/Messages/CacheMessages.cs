using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.Common.MessageContracts.Messages
{
    public class RefreshCacheEvent
    {
        public string messageType { get; set; }

        public RefreshCacheEvent()
        {
            messageType = "RefreshCache";
        }
    }
}
