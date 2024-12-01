using Arahk.Application.Repository;
using Arahk.Application.User.Login;
using Arahk.Application.User.Register;
using Arahk.Domain.Identity.Repositories;
using Arahk.Presentation.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Arahk.Presentation.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class UserIdentityController(ILogger<UserIdentityController> logger, IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpPost(Name = "UserRegister")]
    [Route("UserRegister")]
    public async Task<IActionResult> Register([FromBody] UserRegisterViewModel model)
    {
        var request = new UserRegisterRequest
        {
            Username = model.Username,
            Password = model.Password,
            Email = model.Email
        };

        var handler = new UserRegisterHandler(unitOfWork);
        
        await handler.Handle(request);
        
        return Ok();
    }

    [HttpPost(Name = "UserLogin")]
    [Route("UserLogin")]
    public async Task<IActionResult> Login([FromBody] UserLoginViewModel model)
    {
        var request = new UserLoginRequest
        {
            Username = model.Username,
            Password = model.Password
        };
        
        var handler = new UserLoginHandler(unitOfWork);
        
        var accessToken = await handler.Handle(request);
        
        return Ok(accessToken);
    }
}