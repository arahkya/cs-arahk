using Arahk.Domain.Identity.Repositories;

namespace Arahk.Application.Repository;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    
    public Task SaveAsync();
}