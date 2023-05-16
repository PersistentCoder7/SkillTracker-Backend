using MediatR;
using SkillTracker.Profile.Application.Services.Profile.CommandHandlers;
using SkillTracker.Profile.Application.Services.Profile.Commands;

namespace SkillTracker.Profile.Api.Extensions;

public static class MediatRExtensions
{
    public static void RegisterMediatRCommandHandlers(this IServiceCollection services, ILogger<Program> logger)
    {
        services.AddMediatR(typeof(Program));

        logger.LogInformation("Registering AddProfileCommand,UpdateProfileCommand and GetProfileCommand command handlers");
        //Register Commands 
        services.AddScoped<IRequestHandler<AddProfileCommand, bool>, AddProfileCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProfileCommand, bool>, UpdateProfileCommandHandler>();
        services.AddScoped<IRequestHandler<GetProfileCommand, Domain.Models.Profile>, GetProfileCommandHandler>();

        
    }
}