using MediatR;
using SkillTracker.Search.Domain.Models;
using SkillTracker.Search.Domain.Models.SkillTracker.Search.Api.Models;

namespace SkillTracker.Search.Application.Services.Search.Commands;

public record SearchProfileCommand(SearchCriteria searchCriteria) : IRequest<List<CachedProfile>>;