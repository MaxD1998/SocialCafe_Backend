using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Resources;
using Domain.Base;
using Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseRepository : BaseRepositoryMapper, IBaseRepository
{
    public async Task<bool> CheckRecordExist<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
    {
        using var context = new DataContext();

        return await context.Set<T>()
            .AnyAsync(expression);
    }

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

    public async Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
    {
        using var context = new DataContext();

        var result = await context.Set<T>()
            .FirstOrDefaultAsync(expression);

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

    public async Task<T> GetElementAsync<T>(Expression<Func<T, bool>> expression,
        bool disableAutoInclude = false) where T : BaseEntity
    {
        using var context = new DataContext();

        var query = context.Set<T>()
            .AsNoTracking();

        if (disableAutoInclude)
            query = query.IgnoreAutoIncludes();

        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<T>> GetElementsAsync<T>(Expression<Func<T, bool>> expression,
        bool disableAutoInclude = false) where T : BaseEntity
    {
        using var context = new DataContext();

        var query = context.Set<T>()
            .AsNoTracking()
            .Where(expression);

        if (disableAutoInclude)
            query = query.IgnoreAutoIncludes();

        return await query.ToListAsync();
    }

    public async Task<T> UpdateAsync<T>(Guid id, T entity) where T : BaseEntity
    {
        using var context = new DataContext();

        var result = await context.Set<T>()
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        result.ThrowIfNull(new NotFoundException(ErrorMessages.NoDataToUpdate));
        Map(entity, result);

        await context.SaveChangesAsync();

        return result;
    }

    public async Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        using var context = new DataContext();

        var ids = entities.Select(x => x.Id);
        var results = await context.Set<T>()
            .IgnoreAutoIncludes()
            .Where(x => ids.Contains(x.Id))
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