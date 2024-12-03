using Arahk.Application.Repository;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Membership.Repositories;
using Arahk.Infrastructure.Data;

namespace Arahk.Infrastructure.Repositories;

public class UnitOfWork(ArahkDbContext dbContext, IUserRepository userRepository, IUserProfileRepository userProfileRepository) : IUnitOfWork
{
    public IUserRepository UserRepository { get; } = userRepository;
    public IUserProfileRepository UserProfileRepository { get; } = userProfileRepository; 

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}