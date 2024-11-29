using System.Data;
using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.Repositories;

namespace Arahk.Application.User.Register;

public class UserRegisterHandler(IUserRepository userRepository)
{
    public async Task<UserEntity> Handle(UserRegisterRequest request)
    {
        var user = new UserEntity(Guid.NewGuid(), request.Username, request.Password, request.Email);
        
        if (!user.IsValid()) throw new InvalidDataException();

        if (await userRepository.ExistsAsync(user.Username))
        {
            throw new DuplicateNameException();
        }
        
        await userRepository.AddAsync(user);
        
        return user;
    }
}