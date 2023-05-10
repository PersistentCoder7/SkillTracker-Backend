using MediatR;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Services.Profile.Commands;

public record UpdateProfileCommand (string AssociateId, List<Skill> Skills) : IRequest<bool>;