using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Bases;
using MediatR;
using System.Linq.Expressions;

namespace ApplicationCore.Bases;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly IUnitOfWork _unitOfWork;

    public BaseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        Mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    protected IMapper Mapper { get; set; }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected async Task<TResult> CreateAsync<TEntity, TResult>(object model) where TEntity : BaseEntity
    {
        var entity = Mapper.Map<TEntity>(model);
        var result = await _unitOfWork.BaseRepository
            .CreateAsync(entity);

        return Mapper.Map<TResult>(result);
    }

    protected async Task<IEnumerable<TResult>> CreateRangeAsync<TEntity, TResult>(IEnumerable<object> model) where TEntity : BaseEntity
    {
        var entities = Mapper.Map<IEnumerable<TEntity>>(model);
        var results = await _unitOfWork.BaseRepository
            .CreateRangeAsync(entities);

        return Mapper.Map<IEnumerable<TResult>>(results);
    }

    protected async Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : BaseEntity
        => await _unitOfWork.BaseRepository.DeleteAsync(condition);

    protected async Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(bool disableAutoInclude = false) where TEntity : BaseEntity
    {
        var results = await _unitOfWork.BaseRepository
            .GetAllAsync<TEntity>(disableAutoInclude);

        return Mapper.Map<IEnumerable<TResult>>(results);
    }

    protected async Task<TResult> GetElementAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> condition,
        bool disableAutoInclude = false) where TEntity : BaseEntity
    {
        var result = await _unitOfWork.BaseRepository
            .GetElementAsync(condition, disableAutoInclude);

        return Mapper.Map<TResult>(result);
    }

    protected async Task<TResult> GetElementAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> condition,
        Expression<Func<TEntity, TResult>> selector,
        bool disableAutoInclude = false) where TEntity : BaseEntity
        => await _unitOfWork.BaseRepository.GetElementAsync(condition, selector, disableAutoInclude);

    protected async Task<IEnumerable<TResult>> GetElementsAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> condition,
        bool disableAutoInclude = false) where TEntity : BaseEntity
    {
        var result = await _unitOfWork.BaseRepository
            .GetElementsAsync(condition, disableAutoInclude);

        return Mapper.Map<IEnumerable<TResult>>(result);
    }

    protected async Task<IEnumerable<TResult>> GetElementsAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> condition,
        Expression<Func<TEntity, TResult>> selector,
        bool disableAutoInclude = false) where TEntity : BaseEntity
        => await _unitOfWork.BaseRepository.GetElementsAsync(condition, selector, disableAutoInclude);

    protected async Task<TResult> UpdateAsync<TEntity, TResult>(Guid id, object model) where TEntity : BaseEntity
    {
        var entity = Mapper.Map<TEntity>(model);
        var result = await _unitOfWork.BaseRepository
            .UpdateAsync(id, entity);

        return Mapper.Map<TResult>(result);
    }

    protected async Task<IEnumerable<TResult>> UpdateRangeAsync<TEntity, TResult>(IEnumerable<object> models) where TEntity : BaseEntity
    {
        var entities = Mapper.Map<IEnumerable<TEntity>>(models);
        var results = await _unitOfWork.BaseRepository
            .UpdateRangeAsync(entities);

        return Mapper.Map<IEnumerable<TResult>>(results);
    }
}