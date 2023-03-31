using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Bases;
using System.Linq.Expressions;

namespace ApplicationCore.Bases;

public abstract class BaseRequestHandler
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;

    public BaseRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    protected async Task<TResult> CreateAsync<TEntity, TResult>(object model) where TEntity : BaseEntity
    {
        var entity = _mapper.Map<TEntity>(model);
        var result = await _unitOfWork.BaseRepository
            .CreateAsync(entity);

        return _mapper.Map<TResult>(result);
    }

    protected async Task<IEnumerable<TResult>> CreateRangeAsync<TEntity, TResult>(IEnumerable<object> model) where TEntity : BaseEntity
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(model);
        var results = await _unitOfWork.BaseRepository
            .CreateRangeAsync(entities);

        return _mapper.Map<IEnumerable<TResult>>(results);
    }

    protected async Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : BaseEntity
        => await _unitOfWork.BaseRepository.DeleteAsync(condition);

    protected async Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(bool disableAutoInclude = false) where TEntity : BaseEntity
    {
        var results = await _unitOfWork.BaseRepository
            .GetAllAsync<TEntity>(disableAutoInclude);

        return _mapper.Map<IEnumerable<TResult>>(results);
    }

    protected async Task<TResult> GetElementAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, bool disableAutoInclude = false) where TEntity : BaseEntity
    {
        var result = await _unitOfWork.BaseRepository
            .GetElementAsync(condition, disableAutoInclude);

        return _mapper.Map<TResult>(result);
    }

    protected async Task<TResult> GetElementAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> condition,
        Expression<Func<TEntity, TResult>> selector,
        bool disableAutoInclude = false) where TEntity : BaseEntity
        => await _unitOfWork.BaseRepository.GetElementAsync(condition, selector, disableAutoInclude);

    protected async Task<IEnumerable<TResult>> GetElementsAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, bool disableAutoInclude = false) where TEntity : BaseEntity
    {
        var result = await _unitOfWork.BaseRepository
            .GetElementsAsync(condition, disableAutoInclude);

        return _mapper.Map<IEnumerable<TResult>>(result);
    }

    protected async Task<IEnumerable<TResult>> GetElementsAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> condition,
        Expression<Func<TEntity, TResult>> selector,
        bool disableAutoInclude = false) where TEntity : BaseEntity
        => await _unitOfWork.BaseRepository.GetElementsAsync(condition, selector, disableAutoInclude);

    protected async Task<TResult> UpdateAsync<TEntity, TResult>(Guid id, object model) where TEntity : BaseEntity
    {
        var entity = _mapper.Map<TEntity>(model);
        var result = await _unitOfWork.BaseRepository
            .UpdateAsync(id, entity);

        return _mapper.Map<TResult>(result);
    }

    protected async Task<IEnumerable<TResult>> UpdateRangeAsync<TEntity, TResult>(IEnumerable<object> models) where TEntity : BaseEntity
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        var results = await _unitOfWork.BaseRepository
            .UpdateRangeAsync(entities);

        return _mapper.Map<IEnumerable<TResult>>(results);
    }
}