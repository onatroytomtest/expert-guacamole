using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Faircode.Platform.Data.Sql.ORM.Repositories
{
    public interface IRepository<T> where T : IDataModel
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(long id);
        Task<IEnumerable<T>> GetAllAsyncByFilter(Expression<Func<T, bool>> filter);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllAsyncByFilter<TOrderByVal>(Expression<Func<T, bool>> filter, Func<T, TOrderByVal> orderBy)
            where TOrderByVal : struct;

        Task<IEnumerable<TType>> SelectByFilterAsync<TType>(
           Expression<Func<T, bool>> filter,
           Expression<Func<T, TType>> selector
           ) where TType : class;

        Task<T> SignleOrDefault(long id);


    }
}
