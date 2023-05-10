using MediatR;

namespace SkillTracker.Profile.Application.Services.Profile.Commands;

public record GetProfileCommand(string AssociateId) : IRequest<Domain.Models.Profile>;
