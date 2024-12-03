using Arahk.Application.Repository;
using Arahk.Application.Services;
using Arahk.Domain.Identity.Services;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Application.Usecases.User.Login;

public class UserLoginHandler(IUnitOfWork unitOfWork)
{
    public async Task<string> Handle(UserLoginRequest request)
    {
        var user = await unitOfWork.UserRepository.GetByUserNameAsync(new UsernameValueObject(request.Username));

        if (user == null)
        {
            return string.Empty;
        }
        
        var isPasswordValid = PasswordHashService.VerifyPassword(request.Password, user.HashedPassword.Value);
        var accessToken = TokenService.GenerateUserAccessToken(user); 
        
        return !isPasswordValid ? string.Empty : accessToken;
    }
}