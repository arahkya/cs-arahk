using System.Data;
using Arahk.Application.Repository;
using Arahk.Domain.Core.Common.ValueObjects;
using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Application.User.Register;

public class UserRegisterHandler(IUnitOfWork unitOfWork)
{
    public async Task<UserEntity> Handle(UserRegisterRequest request)
    {
        var user = new UserEntity(Guid.NewGuid(), new UsernameValueObject(request.Username), new PasswordValueObject(request.Password), new EmailValueObject(request.Email));
        
        if (!user.IsValid()) throw new InvalidDataException();

        if (await unitOfWork.UserRepository.ExistsAsync(user.Username))
        {
            throw new DuplicateNameException();
        }
        
        await unitOfWork.UserRepository.AddAsync(user);
        await unitOfWork.SaveAsync();
        
        return user;
    }
}