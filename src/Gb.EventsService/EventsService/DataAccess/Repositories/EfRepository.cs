using DataAccess.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<EfRepository<T>> _logger;

        public EfRepository(DataContext dataContext, ILogger<EfRepository<T>> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dataContext.Set<T>().ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
            return await _dataContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<int> ids)
        {
            var entities = await _dataContext.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
            return entities;
        }

        public async Task<T?> AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);

            int changes = await _dataContext.SaveChangesAsync();

            if (changes > 0)
                return entity;

            return null;
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            try
            {
                int changes = await _dataContext.SaveChangesAsync();

                if (changes > 0)
                    return entity;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error has occured");
            }
            return null;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _dataContext.Set<T>().Remove(entity);
                int changes = await _dataContext.SaveChangesAsync();
                return changes > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
