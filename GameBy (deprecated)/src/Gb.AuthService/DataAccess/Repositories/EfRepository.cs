using DataAccess.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DataContext _dataContext;

        public EfRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
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

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dataContext.Set<T>().AddAsync(entity);

                int changes = await _dataContext.SaveChangesAsync();

                if (changes > 0)
                    return entity;

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                int changes = await _dataContext.SaveChangesAsync();

                return changes > 0;

            }
            catch (Exception ex)
            {
                return false;
            }
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
