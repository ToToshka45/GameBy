using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Abstractions;
using RatingService.Infrastructure.DataAccess;
using System.Linq.Expressions;

namespace RatingService.Infrastructure.Abstractions;

public abstract class BaseRepository<T>(RatingServiceDbContext storage) : IRepository<T> where T : Entity
{
    private readonly DbSet<T> _dbSet = storage.Set<T>();

    public virtual async Task<bool> Add(T entity, CancellationToken token)
    {
        await _dbSet.AddAsync(entity, token);
        return await SaveChangesAsync(token);
    }

    public virtual async Task<bool> Delete(int id, CancellationToken token)
    {
        var entity = await Get(id, token);
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Remove(entity);
        return await SaveChangesAsync(token);
    }

    public virtual async Task<T?> Get(int id, CancellationToken token)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, token);
    }

    public virtual async Task<bool> Update(int id, T entity, CancellationToken token)
    {
        var storedEntity = await Get(id, token);
        ArgumentNullException.ThrowIfNull(entity);
        entity = storedEntity!;
        _dbSet.Update(entity);
        return await SaveChangesAsync(token);
    }
    public async Task<T?> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken token)
    {
        return await _dbSet.FirstOrDefaultAsync(filter, token);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken token)
    {
        return await storage.SaveChangesAsync(token) > 0;
    }

}
