using ApplicationCore.Interfaces;
using Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public async Task<T> CreateAsync<T>(T entity) where T : BaseEntity
        {
            using (var context = new DataContext())
            {
                var result = await context.Set<T>()
                    .AddAsync(entity);
                await context.SaveChangesAsync();

                return result.Entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity
        {
            using (var context = new DataContext())
            {
                return await context.Set<T>()
                    .ToListAsync();
            }
        }

        public async Task<T> GetElement<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            using (var context = new DataContext())
            {
                return await context.Set<T>()
                    .FirstOrDefaultAsync(expression);
            }
        }

        public async Task<IEnumerable<T>> GetElements<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            using (var context = new DataContext())
            {
                return await context.Set<T>()
                    .Where(expression)
                    .ToListAsync();
            }
        }
    }
}