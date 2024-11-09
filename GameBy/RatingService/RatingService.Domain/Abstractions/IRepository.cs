namespace RatingService.Domain.Abstractions;
public interface IRepository<T> where T : BaseEntity
{
    Task<bool> SaveChangesAsync();
    Task Add(T entity);
    Task Get(T entity);
    Task Update(int id, T entity);
    Task Delete(int id);
}
