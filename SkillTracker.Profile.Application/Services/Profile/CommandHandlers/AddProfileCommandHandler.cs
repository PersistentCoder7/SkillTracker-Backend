using MediatR;
using Microsoft.Extensions.Logging;
using SkillTracker.Profile.Application.Exceptions;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Services.Profile.Commands;

namespace SkillTracker.Profile.Application.Services.Profile.CommandHandlers;

public class AddProfileCommandHandler : IRequestHandler<AddProfileCommand, bool>
{
    private readonly IProfileService _service;
    private readonly ILogger<AddProfileCommandHandler> _logger;

    public AddProfileCommandHandler(IProfileService service, ILogger<AddProfileCommandHandler> logger)
    {
        _service = service;
        _logger = logger;
    }
    public async Task<bool> Handle(AddProfileCommand request, CancellationToken cancellationToken)
    {
        //TODO: (int)StatusCodes.Status409Conflict
        var profile = await _service.GetProfile(request.AssociateId);
        if (profile != null) throw new ProfileAlreadyExistsException("The associate profile already exists.");
        
        //_logger.LogInformation($"Publishing  [AddedProfileEvent] for Associate: {request.AssociateId}");
        //_bus.Publish(new AddedProfileEvent()
        //{
        //    Name = request.Name,
        //    Email = request.Email,
        //    AssociateId = request.AssociateId,
        //    Skills = request.Skills,
        //    Mobile = request.Mobile
        //});

        return await Task.FromResult(true);
    }
}