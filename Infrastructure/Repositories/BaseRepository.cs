using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Resources;
using Domain.Bases;
using Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseRepository : BaseRepositoryTool, IBaseRepository
{
    public async Task<T> CreateAsync<T>(T entity) where T : BaseEntity
    {
        using var context = new DataContext();

        var result = await context.Set<T>()
            .AddAsync(entity);

        await context.SaveChangesAsync();

        return await context.Set<T>()
            .FirstOrDefaultAsync(x => x.Id.Equals(result.Entity.Id));
    }

    public async Task<IEnumerable<T>> CreateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        using var context = new DataContext();

        await context.Set<T>()
            .AddRangeAsync(entities);
        await context.SaveChangesAsync();

        return entities;
    }

    public async Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : BaseEntity
    {
        using var context = new DataContext();

        var result = await context.Set<T>()
            .FirstOrDefaultAsync(condition);

        if (result is null)
            throw new NotFoundException();

        context.Set<T>()
            .Remove(result);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(bool disableAutoInclude = false) where T : BaseEntity
    {
        using var context = new DataContext();

        var query = context.Set<T>()
            .AsNoTracking();

        if (disableAutoInclude)
            query = query.IgnoreAutoIncludes();

        return await query.ToListAsync();
    }

    public async Task<T> GetElementAsync<T>(Expression<Func<T, bool>> condition,
        bool disableAutoInclude = false) where T : BaseEntity
    {
        using var context = new DataContext();
        var query = GetQueryNoTracking(context, condition, disableAutoInclude);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<TResult> GetElementAsync<T, TResult>(
        Expression<Func<T, bool>> condition,
        Expression<Func<T, TResult>> selector,
        bool disableAutoInclude = false) where T : BaseEntity
    {
        using var context = new DataContext();
        var query = GetQueryNoTracking(context, condition, disableAutoInclude);
        var select = query.Select(selector);

        return await select.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetElementsAsync<T>(Expression<Func<T, bool>> condition,
        bool disableAutoInclude = false) where T : BaseEntity
    {
        using var context = new DataContext();
        var query = GetQueryNoTracking(context, condition, disableAutoInclude);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TResult>> GetElementsAsync<T, TResult>(
        Expression<Func<T, bool>> condition,
        Expression<Func<T, TResult>> selector,
        bool disableAutoInclude = false) where T : BaseEntity
    {
        using var context = new DataContext();
        var query = GetQueryNoTracking(context, condition, disableAutoInclude);
        var select = query.Select(selector);

        return await select.ToListAsync();
    }

    public async Task<bool> IsExist<T>(Expression<Func<T, bool>> condition) where T : BaseEntity
    {
        using var context = new DataContext();

        return await context.Set<T>()
            .AnyAsync(condition);
    }

    public async Task<T> UpdateAsync<T>(Guid id, T entity) where T : BaseEntity
    {
        using var context = new DataContext();
        var query = GetQuery<T>(context, x => x.Id.Equals(id), true);
        var result = await query
            .FirstOrDefaultAsync();

        result.ThrowIfNull(new NotFoundException(ErrorMessages.NoDataToUpdate));
        Map(entity, result);

        await context.SaveChangesAsync();

        return result;
    }

    public async Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        using var context = new DataContext();

        var ids = entities.Select(x => x.Id);
        var query = GetQuery<T>(context, x => ids.Contains(x.Id), true);
        var results = await query
            .ToListAsync();

        results.ThrowIfNullOrEmpty(new NotFoundException(ErrorMessages.NoDataToUpdate));

        foreach (var result in results)
        {
            var entity = entities.FirstOrDefault(x => x.Id.Equals(result.Id));
            Map(entity, result);
        }

        await context.SaveChangesAsync();

        return results;
    }
}