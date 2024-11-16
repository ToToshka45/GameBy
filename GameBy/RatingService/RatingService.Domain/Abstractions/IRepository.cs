using RatingService.Domain.Primitives;
using System.Linq.Expressions;

namespace RatingService.Domain.Abstractions;
public interface IRepository<T, TId> where T : AggregateRoot<TId> where TId : notnull
{
    Task<bool> SaveChangesAsync(CancellationToken token);
    Task<bool> Add(T entity, CancellationToken token);
    Task<T?> Get(TId id, CancellationToken token);
    Task<T?> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken token);
    Task<bool> Update(TId id, T entity, CancellationToken token);
    Task<bool> Delete(TId id, CancellationToken token);
}
