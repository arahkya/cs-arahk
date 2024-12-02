using Arahk.Application.Repository;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Identity.Services;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Application.User.ChangePassword;

public class UserChangePasswordHandler(IUnitOfWork unitOfWork)
{
    public async Task Handle(UserChangePasswordRequest request)
    {
        var user = await unitOfWork.UserRepository.GetByUserNameAsync(new UsernameValueObject(request.Username));
        
        if (user == null)
        {
            return;
        }
        
        var isPasswordValid = PasswordHashService.VerifyPassword(request.OldPassword, user.HashedPassword.Value);
        
        if (!isPasswordValid)
        {
            return;
        }

        user.ChangePassword(request.NewPassword);
        
        await unitOfWork.UserRepository.UpdateAsync(user);
    }
}