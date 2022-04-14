using Domain.Base;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces
{
    public interface IBaseRepository
    {
        Task<T> CreateAsync<T>(T entity) where T : BaseEntity;

        Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity;

        Task<T> GetElement<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;

        Task<IEnumerable<T>> GetElements<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
    }
}