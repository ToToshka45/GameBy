using System.Linq.Expressions;

namespace RatingService.Domain.Abstractions;
public interface IRepository<T> where T : BaseEntity
{
    Task<bool> SaveChangesAsync(CancellationToken token);
    Task<bool> Add(T entity, CancellationToken token);
    Task<T?> Get(int id, CancellationToken token);
    Task<T?> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken token);
    Task<bool> Update(int id, T entity, CancellationToken token);
    Task<bool> Delete(int id, CancellationToken token);
}
