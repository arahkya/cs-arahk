using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Identity.ValueObjects;
using Arahk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Arahk.Infrastructure.Repositories;

public class UserRepository(ArahkDbContext dbContext) : BaseRepository<Guid, UserEntity>(dbContext), IUserRepository
{
    private readonly ArahkDbContext _dbContext = dbContext;

    public async Task<bool> ExistsAsync(UsernameValueObject username)
    {
        var existedUsers = await _dbContext.Set<UserEntity>().AnyAsync(p => p.Username == new UsernameValueObject(username.Value));

        return existedUsers;
    }

    public async Task<UserEntity?> GetByUserNameAsync(UsernameValueObject username)
    {
        return await _dbContext.Set<UserEntity>().SingleOrDefaultAsync(p => p.Username == new UsernameValueObject(username.Value));
    }
}