using MediatR;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Services.Profile.Commands;

public record AddProfileCommand
    (string AssociateId, string Name, string Email, string Mobile, List<SkillProficiency> Skills) : IRequest<bool>;
