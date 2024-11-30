using Arahk.Application.Repository;
using Arahk.Domain.Identity.Repositories;
using Arahk.Infrastructure.Data;

namespace Arahk.Infrastructure.Repositories;

public class UnitOfWork(ArahkDbContext dbContext, IUserRepository userRepository) : IUnitOfWork
{
    public IUserRepository UserRepository { get; } = userRepository;
    
    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}