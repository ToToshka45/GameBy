using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Abstractions;
using RatingService.Domain.Primitives;
using RatingService.Infrastructure.DataAccess;
using System.Linq.Expressions;

namespace RatingService.Infrastructure.Abstractions;

public class BaseRepository<T>(RatingServiceDbContext storage) 
    : IRepository<T> where T : Entity<int>
{
    private readonly DbSet<T> _dbSet = storage.Set<T>();

    public virtual async Task<T> Add(T entity, CancellationToken token)
    {
        var result = await _dbSet.AddAsync(entity, token);
        await SaveChangesAsync(token);
        return result.Entity;
    }

    public virtual async Task<bool> Delete(int id, CancellationToken token)
    {
        var entity = await GetById(id, token);
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Remove(entity);
        return await SaveChangesAsync(token);
    }

    public virtual async Task<ICollection<T>> GetAll(CancellationToken token)
    {
        return await _dbSet.AsNoTracking().ToListAsync(token);
    }

    public virtual async Task<T?> GetById(int id, CancellationToken token)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, token);
    }
    public async Task<T?> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken token)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter, token);
    }

    public virtual async Task<bool> Update(int id, T entity, CancellationToken token)
    {
        var storedEntity = await GetById(id, token);
        ArgumentNullException.ThrowIfNull(entity);
        entity = storedEntity!;
        _dbSet.Update(entity);
        return await SaveChangesAsync(token);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken token)
    {
        return await storage.SaveChangesAsync(token) > 0;
    }

}
