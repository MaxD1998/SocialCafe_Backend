using Domain.Base;

namespace Infrastructure.Bases;

public class BaseRepositoryMapper
{
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