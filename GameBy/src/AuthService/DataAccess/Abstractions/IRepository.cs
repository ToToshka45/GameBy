using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstractions
{
    public  interface IRepository<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получает объект по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns>
        /// Объект типа <typeparamref name="T"/> если он найден; в противном случае <c>null</c>.
        /// </returns>
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids);

        Task<T> AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);
    }
}
