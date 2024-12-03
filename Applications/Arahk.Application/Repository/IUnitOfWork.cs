using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Membership.Repositories;

namespace Arahk.Application.Repository;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    
    public IUserProfileRepository UserProfileRepository { get; }
    
    public Task SaveAsync();
}