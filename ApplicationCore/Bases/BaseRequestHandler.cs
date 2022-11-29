﻿using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Base;
using System.Linq.Expressions;

namespace ApplicationCore.Bases
{
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

        protected async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : BaseEntity
            => await _unitOfWork.BaseRepository.DeleteAsync<TEntity>(id);

        protected async Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(bool disableAutoInclude = false) where TEntity : BaseEntity
        {
            var results = await _unitOfWork.BaseRepository
                .GetAllAsync<TEntity>(disableAutoInclude);

            return _mapper.Map<IEnumerable<TResult>>(results);
        }

        protected async Task<TResult> GetElementAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> expression, bool disableAutoInclude = false) where TEntity : BaseEntity
        {
            var result = await _unitOfWork.BaseRepository
                .GetElementAsync(expression, disableAutoInclude);

            return _mapper.Map<TResult>(result);
        }

        protected async Task<IEnumerable<TResult>> GetElementsAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> expression, bool disableAutoInclude = false) where TEntity : BaseEntity
        {
            var result = await _unitOfWork.BaseRepository
                .GetElementsAsync(expression, disableAutoInclude);

            return _mapper.Map<IEnumerable<TResult>>(result);
        }

        protected async Task<TResult> UpdateAsync<TEntity, TResult>(int id, object model) where TEntity : BaseEntity
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
}