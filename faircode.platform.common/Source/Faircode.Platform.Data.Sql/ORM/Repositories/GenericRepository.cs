using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Faircode.Platform.Data.Sql.ORM.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, IDataModel
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<T> AddAsync(T entity)
        {
            var res = await _dbContext.Set<T>().AddAsync(entity);
            _dbContext.SaveChanges();
            return res.Entity;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await SignleOrDefault(id);
            if (entity == null)
            {
                throw new ArgumentException($"Invalid Id {id}");
            }

            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            var entities = await GetAllAsyncByFilter(filter);
            if (entities == null || !entities.Any())
            {
                return;
            }
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncByFilter(Expression<Func<T, bool>> filter)
        {
            return await Task.FromResult(_dbContext.Set<T>().Where(filter).ToList());
        }

        public Task<IEnumerable<T>> GetAllAsyncByFilter<TOrderByVal>(Expression<Func<T, bool>> filter, Func<T, TOrderByVal> orderBy) where TOrderByVal : struct
        {
            return Task.FromResult<IEnumerable<T>>(_dbContext.Set<T>()
                .Where(filter).OrderBy(orderBy));
        }

        public Task<IEnumerable<TType>> SelectByFilterAsync<TType>(Expression<Func<T, bool>> filter, Expression<Func<T, TType>> selector) where TType : class
        {
            return Task.FromResult<IEnumerable<TType>>(_dbContext.Set<T>().Where(filter).Select(selector));
        }

        public Task<T> SignleOrDefault(long id)
        {
            return _dbContext.Set<T>().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var current = await SignleOrDefault(entity.Id);
            if (current == null)
            {
                throw new ArgumentException($"Invalid Id {entity.Id}");
            }
            _dbContext.Entry<T>(current).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
