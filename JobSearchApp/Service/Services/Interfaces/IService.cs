using System;
using Domain.Commons;
using System.Linq.Expressions;
using Service.Helpers;

namespace Service.Services.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        Task<ResponseObj> Create(T entity);
        Task<ResponseObj> Delete(int id);
        Task<ResponseObj> DeleteFromDB(int id);
        Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task<bool> IsExist(Expression<Func<T, bool>> predicate = null);
        Task<ResponseObj> Update(T entity);
    }
}

