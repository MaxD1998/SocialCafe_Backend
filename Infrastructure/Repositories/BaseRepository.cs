using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Resources;
using Domain.Base;
using Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
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

        public async Task<bool> DeleteAsync<T>(int id) where T : BaseEntity
        {
            using var context = new DataContext();

            var result = await context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (result is null)
                return false;

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

        public async Task<T> UpdateAsync<T>(int id, T entity) where T : BaseEntity
        {
            using var context = new DataContext();

            var record = await context.Set<T>()
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            record.ThrowIfNull(new NotFoundException(ErrorMessages.NoDataToUpdate));
            Map(entity, record);

            var result = context.Set<T>()
                .Update(record);

            await context.SaveChangesAsync();

            return await context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(result.Entity.Id));
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            using var context = new DataContext();

            var ids = entities.Select(x => x.Id);
            var records = await context.Set<T>()
                .IgnoreAutoIncludes()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            records.ThrowIfNullOrEmpty(new NotFoundException(ErrorMessages.NoDataToUpdate));

            foreach (var record in records)
            {
                var entity = entities.FirstOrDefault(x => x.Id.Equals(record.Id));
                Map(entity, record);
            }

            context.Set<T>()
                .UpdateRange(records);

            await context.SaveChangesAsync();

            return entities;
        }
    }
}