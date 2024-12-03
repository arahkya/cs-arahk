using Arahk.Application.Repository;
using Arahk.Application.Usecases.Membership;
using Arahk.Presentation.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Arahk.Presentation.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class UserProfileController(ILogger<UserProfileController> logger, IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly ILogger<UserProfileController> _logger = logger;
    
    [HttpPatch(Name = "UpdateUserProfile")]
    public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileViewModel model)
    {
        var request = new UpdateUserProfileRequest
        {
            Salutation = model.Salutation!,
            Firstname = model.Firstname!,
            Lastname = model.Lastname!,
            Gender = model.Gender!,
            PrimaryMobilePhone = model.PrimaryMobilePhone!,
            Username = model.Username!
        };

        var handler = new UpdateUserProfileHandler(unitOfWork);

        await handler.Handle(request);
        
        return Ok();
    }
}