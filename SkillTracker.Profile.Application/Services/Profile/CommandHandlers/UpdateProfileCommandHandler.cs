using MediatR;
using Microsoft.Extensions.Logging;
using SkillTracker.Profile.Application.Exceptions;
using SkillTracker.Profile.Application.Interfaces;
using SkillTracker.Profile.Application.Services.Profile.Commands;
using SkillTracker.Profile.Domain.Models;

namespace SkillTracker.Profile.Application.Services.Profile.CommandHandlers;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
{
    
    private readonly IProfileService _service;
    private readonly ILogger<UpdateProfileCommandHandler> _logger;

    public UpdateProfileCommandHandler(IProfileService service, ILogger<UpdateProfileCommandHandler> logger)
    {
        _service = service;
        _logger = logger;
    }
    public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        //Check if the profile exists in the database
        var associateId = request.AssociateId;
        var profile = await _service.GetProfile(associateId);
        if (profile == null)  throw new NotFoundException("The associate profile was not found.");
        
        var currentDate = DateTime.Now;

        //If the profile wasn't updated ever
        if (profile.UpdatedOn == null && currentDate.Subtract(profile.AddedOn!.Value).Days <= 10)
            throw new CustomValidationException("The profile can be updated only after 10 days of adding the profile");
        
        if (profile.UpdatedOn != null && currentDate.Subtract(profile.UpdatedOn!.Value).Days <= 10) 
            throw new CustomValidationException("The profile can be updated only after 10 days of updating the profile");

        await _service.UpdateProfile(MapTo(request));
        return await Task.FromResult(true);
    }

    private UpdateProfile MapTo(UpdateProfileCommand request) => new UpdateProfile()
        { AssociateId = request.AssociateId, Skills = request.Skills,UpdatedOn = DateTime.Now};
}