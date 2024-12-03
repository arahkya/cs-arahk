using Arahk.Application.Repository;
using Arahk.Application.Usecases.User.ChangePassword;
using Arahk.Application.Usecases.User.Login;
using Arahk.Application.Usecases.User.Register;
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
    
    [HttpPost(Name = "UserChangePassword")]
    [Route("UserChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordViewModel model)
    {
        var request = new UserChangePasswordRequest
        {
            Username = model.Username,
            OldPassword = model.OldPassword,
            NewPassword = model.NewPassword
        };
        
        var handler = new UserChangePasswordHandler(unitOfWork);
        
        await handler.Handle(request);
        
        return Ok();
    }
}