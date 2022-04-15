using Domain.Base;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        Task<T> CreateAsync<T>(T entity) where T : BaseEntity;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseEntity;

        Task<T> GetElementAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;

        Task<IEnumerable<T>> GetElementsAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
    }
}