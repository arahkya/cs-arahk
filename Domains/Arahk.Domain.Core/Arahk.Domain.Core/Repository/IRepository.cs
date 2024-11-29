using Arahk.Domain.Core.Entity;

namespace Arahk.Domain.Core.Repository;

public interface IRepository<in TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    Task<TEntity> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}