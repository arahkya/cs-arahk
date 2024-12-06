using Arahk.Domain.Membership.Entities;
using Arahk.Domain.Membership.Repositories;
using Arahk.Domain.Membership.ValueObjects;
using Arahk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Arahk.Infrastructure.Repositories;

public class UserProfileRepository(ArahkDbContext dbContext)  
    : BaseRepository<Guid, UserProfileEntity>(dbContext), IUserProfileRepository
{
    private readonly ArahkDbContext _dbContext = dbContext;

    public async Task<UserProfileEntity?> GetByUserIdentityIdAsync(Guid userId)
    {
        return await _dbContext.UserProfiles.SingleOrDefaultAsync(p => p.UserIdentityId == userId);
    }
}