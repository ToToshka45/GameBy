using RatingService.Domain.Primitives;
using System.Linq.Expressions;

namespace RatingService.Domain.Abstractions;
public interface IRepository<T> where T : Entity<int>
{
    Task<T?> Add(T entity, CancellationToken token);
    Task<ICollection<T>> GetAll(CancellationToken token);
    Task<T?> GetById(int id, CancellationToken token, bool asNoTracking = true);
    Task<T?> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken token);
    Task<bool> Update(int id, T entity, CancellationToken token);
    Task<bool> Delete(int id, CancellationToken token);
    Task<T?> GetEntityWithIncludesAsync(int id, CancellationToken token, params Expression<Func<T?, object>>[] includes);
    Task<bool> SaveChangesAsync(CancellationToken token);
}
