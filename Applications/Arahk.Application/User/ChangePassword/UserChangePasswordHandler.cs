using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Identity.Services;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Application.User.ChangePassword;

public class UserChangePasswordHandler(IUserRepository userRepository)
{
    public async Task Handle(UserChangePasswordRequest request)
    {
        var user = await userRepository.GetByUserNameAsync(new UsernameValueObject(request.Username));
        
        if (user == null)
        {
            throw new ApplicationException("User not found");
        }
        
        var isPasswordValid = PasswordHashService.VerifyPassword(request.OldPassword, user.HashedPassword.Value);
        
        if (!isPasswordValid)
        {
            throw new ApplicationException("Invalid password");
        }

        user.ChangePassword(request.NewPassword);
        
        await userRepository.UpdateAsync(user);
    }
}