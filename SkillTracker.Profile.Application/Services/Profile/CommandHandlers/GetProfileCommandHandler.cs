using MediatR;
using Microsoft.Extensions.Logging;
using SkillTracker.Profile.Application.Exceptions;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Services.Profile.Commands;

namespace SkillTracker.Profile.Application.Services.Profile.CommandHandlers;

public class GetProfileCommandHandler : IRequestHandler<GetProfileCommand, Domain.Models.Profile>
{
    private readonly IProfileService _service;
    private readonly ILogger<GetProfileCommandHandler> _logger;

    public GetProfileCommandHandler(IProfileService service, ILogger<GetProfileCommandHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Domain.Models.Profile> Handle(GetProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _service.GetProfile(request.AssociateId);
        if (profile == null) throw new NotFoundException("The associate profile is not found.");
        
        return await Task.FromResult(profile);
    }
}