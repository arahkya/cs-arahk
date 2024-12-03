using Arahk.Application.Services;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Identity.Services;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Application.Usecases.User.CreateAccessToken;

public class UserCreateAccessTokenHandler(IUserRepository userRepository)
{
    public async Task<string> Handle(UserCreateAccessTokenRequest request)
    {
        var user = await userRepository.GetByUserNameAsync(new UsernameValueObject(request.Username));
        
        if (user == null)
        {
            return string.Empty;
        }
        
        var isPasswordValid = PasswordHashService.VerifyPassword(request.Password, user.HashedPassword.Value);
        
        if (!isPasswordValid)
        {
            return string.Empty;
        }

        var accessToken = TokenService.GenerateUserAccessToken(user);
        
        return accessToken;
    }
}