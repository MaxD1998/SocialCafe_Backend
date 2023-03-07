using Domain.Base;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces.Repositories;

public interface IBaseRepository
{
    Task<bool> CheckRecordExist<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;

    Task<T> CreateAsync<T>(T entity) where T : BaseEntity;

    Task<IEnumerable<T>> CreateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity;

    Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;

    Task<IEnumerable<T>> GetAllAsync<T>(bool disableAutoInclude = false) where T : BaseEntity;

    Task<T> GetElementAsync<T>(Expression<Func<T, bool>> expression, bool disableAutoInclude = false) where T : BaseEntity;

    Task<IEnumerable<T>> GetElementsAsync<T>(Expression<Func<T, bool>> expression, bool disableAutoInclude = false) where T : BaseEntity;

    Task<T> UpdateAsync<T>(Guid id, T entity) where T : BaseEntity;

    Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity;
}