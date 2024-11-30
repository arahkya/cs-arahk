using Arahk.Domain.Core.Entity;
using Arahk.Domain.Core.Repository;
using Arahk.Infrastructure.Data;

namespace Arahk.Infrastructure.Repositories;

public abstract class BaseRepository<TKey, TEntity>(ArahkDbContext dbContext) : IRepository<TKey, TEntity> 
    where TEntity : BaseEntity<TKey>
{
    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        var entity = await dbContext.Set<TEntity>().FindAsync(id);

        if (entity == null)
        {
            throw new NullReferenceException();
        }
        
        return entity;
    }

    public async Task AddAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
        
        return Task.CompletedTask;
    }
}