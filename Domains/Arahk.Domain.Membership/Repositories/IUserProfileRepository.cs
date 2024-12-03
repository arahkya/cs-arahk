using Arahk.Domain.Core.Repository;
using Arahk.Domain.Membership.Entities;

namespace Arahk.Domain.Membership.Repositories;

public interface IUserProfileRepository : IRepository<Guid, UserProfileEntity>
{
    Task<UserProfileEntity?> GetByUserIdentityIdAsync(Guid userId);
}