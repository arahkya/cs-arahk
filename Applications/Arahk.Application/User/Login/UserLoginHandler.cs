using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Identity.Services;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Application.User.Login;

public class UserLoginHandler(IUserRepository userRepository)
{
    public async Task<UserEntity?> Handle(UserLoginRequest request)
    {
        var user = await userRepository.GetByUserNameAsync(new UsernameValueObject(request.Username));

        if (user == null)
        {
            return null;
        }
        
        var isPasswordValid = PasswordHashService.VerifyPassword(request.Password, user.HashedPassword.Value);
        
        return !isPasswordValid ? null : user;
    }
}