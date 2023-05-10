using MediatR;

using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Services;
using SkillTracker.Profile.Application.Services.Profile.CommandHandlers;
using SkillTracker.Profile.Application.Services.Profile.Commands;
using SkillTracker.Profile.Data.DbContext;
using SkillTracker.Profile.Data.Repository;
using SkillTracker.Profile.Infrastructure.Interfaces;

namespace SkillTracker.Profile.Api.Extensions;

public static class MicroserviceExtensions
{
    public static void RegisterMicroServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Program));

        //Domain Bus
        //services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
        //{
        //    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
        //    return new RabbitMQBus(mediator: sp.GetService<IMediator>(), serviceScopeFactory: scopeFactory);
        //});

  
  
        //Register Commands 
        services.AddTransient<IRequestHandler<AddProfileCommand, bool>, AddProfileCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateProfileCommand, bool>, UpdateProfileCommandHandler>();
        

        //Application Services
        services.AddTransient<IProfileService, ProfileService>();

        //DataSource
        services.AddTransient<IProfileRepository, ProfileRepository>();
        services.AddTransient<ProfileDbContext>();
    }

    public static void EnListSubscribeToEventBus(this WebApplication webApplication)
    {
        //var eventBus = webApplication.Services.GetRequiredService<IEventBus>();
        //eventBus.Subscribe<AddedProfileEvent, AddedProfileEventHandler>();
        //eventBus.Subscribe<UpdatedProfileEvent, UpdatedProfileEventHandler>();
        //eventBus.Subscribe<SearchProfileEvent, SearchProfileEventHandler>();
    }

}