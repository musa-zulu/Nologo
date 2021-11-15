using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nologo.Domain.Common;

namespace Nologo.Service.Contracts
{
    public interface IGenericService<T> where T : class
    {
        Task<Response<T>> GetByIdAsync(Guid Id);
        Task<Response<IEnumerable<T>>> GetAllAsync();
        Task<Response<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> expression);
        Task<Response<bool>> AddAsync(T entity);
        Task<Response<bool>> AddRangeAsync(IEnumerable<T> entities);
        Task<Response<bool>> RemoveAsync(T entity);
        Task<Response<bool>> RemoveRangeAsync(IEnumerable<T> entities);
        Task<Response<bool>> UpdateAsync(T entity);
    }
}
