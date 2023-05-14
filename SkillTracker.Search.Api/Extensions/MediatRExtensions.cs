using MediatR;
using SkillTracker.Profile.Application.Services.Profile.CommandHandlers;
using SkillTracker.Profile.Application.Services.Profile.Commands;
using SkillTracker.Search.Application.Services.Search.CommandHandlers;
using SkillTracker.Search.Application.Services.Search.Commands;
using SkillTracker.Search.Domain.Models;

namespace SkillTracker.Search.Api.Extensions;

public static class MediatRExtensions
{
    public static void RegisterMediatRCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Program));

        //Register Commands 
        services.AddScoped<IRequestHandler<SearchProfileCommand, List<CachedProfile>>, SearchProfileCommandHandler>();
    }
}