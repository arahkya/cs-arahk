using Arahk.Domain.Core.Repository;
using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.ValueObjects;

namespace Arahk.Domain.Identity.Repositories;

public interface IUserRepository : IRepository<Guid, UserEntity>
{
    public Task<bool> ExistsAsync(UsernameValueObject username);
    
    public Task<UserEntity?> GetByUserNameAsync(UsernameValueObject username);
}