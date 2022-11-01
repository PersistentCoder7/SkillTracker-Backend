using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Infrastructure.Bus;

namespace SkillTracker.Infrastructure.IoC
{
    public  class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
