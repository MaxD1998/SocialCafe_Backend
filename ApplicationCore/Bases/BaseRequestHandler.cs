using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Base;
using System.Linq.Expressions;

namespace ApplicationCore.Bases
{
    public abstract class BaseRequestHandler
    {
        public BaseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        protected IMapper Mapper { get; }

        protected IUnitOfWork UnitOfWork { get; }

        protected async Task<TResult> CreateAsync<TEntity, TResult>(object model) where TEntity : BaseEntity
        {
            var entity = Mapper.Map<TEntity>(model);
            var result = await UnitOfWork.BaseRepository
                .CreateAsync(entity);

            return Mapper.Map<TResult>(result);
        }

        protected async Task<IEnumerable<TResult>> CreateRangeAsync<TEntity, TResult>(IEnumerable<object> model) where TEntity : BaseEntity
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(model);
            var results = await UnitOfWork.BaseRepository
                .CreateRangeAsync(entities);

            return Mapper.Map<IEnumerable<TResult>>(results);
        }

        protected async Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>() where TEntity : BaseEntity
        {
            var results = await UnitOfWork.BaseRepository
                .GetAllAsync<TEntity>();

            return Mapper.Map<IEnumerable<TResult>>(results);
        }

        protected async Task<TResult> GetElementAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity
        {
            var result = await UnitOfWork.BaseRepository
                .GetElementAsync(expression);

            return Mapper.Map<TResult>(result);
        }

        protected async Task<IEnumerable<TResult>> GetElementsAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> expression) where TEntity : BaseEntity
        {
            var result = await UnitOfWork.BaseRepository
                .GetElementsAsync(expression);

            return Mapper.Map<IEnumerable<TResult>>(result);
        }

        protected async Task<TResult> UpdateAsync<TEntity, TResult>(int id, object model) where TEntity : BaseEntity
        {
            var entity = Mapper.Map<TEntity>(model);
            var result = await UnitOfWork.BaseRepository
                .UpdateAsync(id, entity);

            return Mapper.Map<TResult>(result);
        }

        protected async Task<IEnumerable<TResult>> UpdateRangeAsync<TEntity, TResult>(IEnumerable<object> models) where TEntity : BaseEntity
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(models);
            var results = await UnitOfWork.BaseRepository
                .UpdateRangeAsync(entities);

            return Mapper.Map<IEnumerable<TResult>>(results);
        }
    }
}