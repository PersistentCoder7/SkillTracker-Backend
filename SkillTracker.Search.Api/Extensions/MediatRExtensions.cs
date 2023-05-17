using MediatR;
using SkillTracker.Search.Application.Interfaces;
using SkillTracker.Search.Application.Services;
using SkillTracker.Search.Cache.Interfaces;
using SkillTracker.Search.Cache;
using SkillTracker.Search.Domain.Models;

namespace SkillTracker.Search.Api.Extensions;

public static class MediatRExtensions
{
    public static void RegisterMediatRCommandHandlers(this IServiceCollection services,ILogger<Program> logger)
    {
        logger.LogInformation("Registering Service,Cache, SearchProfileCommand with MediatR");
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<ICacheRepository, CacheRepository>();

        services.AddMediatR(typeof(Program));
    }
}