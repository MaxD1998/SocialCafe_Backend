using Domain.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Bases;

public class BaseRepositoryTool
{
    protected IQueryable<T> GetQuery<T>(DataContext context,
        Expression<Func<T, bool>> condition,
        bool disableAutoInclude = false) where T : BaseEntity
    {
        var query = context.Set<T>()
        .Where(condition);

        if (disableAutoInclude)
            query = query.IgnoreAutoIncludes();

        return query;
    }

    protected IQueryable<T> GetQueryNoTracking<T>(DataContext context,
        Expression<Func<T, bool>> condition,
        bool disableAutoInclude = false) where T : BaseEntity
        => GetQuery(context, condition, disableAutoInclude)
        .AsNoTracking();

    protected void Map<T>(T source, T dest) where T : BaseEntity
    {
        var baseProperties = typeof(BaseEntity).GetProperties();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var isBase = false;

            foreach (var baseProperty in baseProperties)
            {
                if (property.Name.Equals(baseProperty.Name))
                {
                    isBase = true;
                    break;
                }
            }

            if (isBase)
                continue;

            typeof(T).GetProperty(property.Name)
                .SetValue(dest, property.GetValue(source));
        }
    }
}