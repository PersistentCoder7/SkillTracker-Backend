using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Infrastructure.Bus;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Services;
using SkillTracker.Profile.Data.DbContext;
using SkillTracker.Profile.Data.Repository;
using SkillTracker.Profile.Domain.CommandHandlers;
using SkillTracker.Profile.Domain.Commands;
using SkillTracker.Profile.Domain.EventHandlers;
using SkillTracker.Profile.Domain.Events;
using SkillTracker.Profile.Domain.Interfaces;

namespace SkillTracker.Infrastructure.IoC
{
    public static  class DependencyContainer
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {

            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });
            
            //Subscriptions
            services.AddTransient<AddedProfileEventHandler>();
            services.AddTransient<UpdatedProfileEventHandler>();

            //Register Events
            services.AddTransient<IEventHandler<AddedProfileEvent>, AddedProfileEventHandler>();
            services.AddTransient<IEventHandler<UpdatedProfileEvent>, UpdatedProfileEventHandler>();

            //Register Commands 
            services.AddTransient<IRequestHandler<AddProfileCommand, bool>, AddProfileCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateProfileCommand, bool>, UpdateProfileCommandHandler>();


            //Application Services
            services.AddTransient<IProfileService, ProfileService>();

            //DataSource
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<ProfileDbContext>();
        }
    }
}
