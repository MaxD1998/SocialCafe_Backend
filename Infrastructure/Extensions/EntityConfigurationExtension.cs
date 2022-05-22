using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Infrastructure.Extensions
{
    public static class EntityConfigurationExtension
    {
        public static PropertyBuilder SetProperty<T>(this EntityTypeBuilder<T> modelBuilder,
            Expression<Func<T, object>> property,
            int? stringLenght = null,
            bool requiered = false) where T : class
        {
            var builder = modelBuilder.Property(property);

            if (stringLenght.HasValue)
            {
                builder.HasMaxLength(stringLenght.Value);
            }

            if (requiered)
            {
                builder.IsRequired();
            }

            return builder;
        }

        public static PropertyBuilder SetProperty<T, TRelated>(this OwnedNavigationBuilder<T, TRelated> modelBuilder,
            Expression<Func<TRelated, object>> property,
            int? stringLenght = null,
            bool requiered = false) where T : class where TRelated : class
        {
            var builder = modelBuilder.Property(property);

            if (stringLenght.HasValue)
            {
                builder.HasMaxLength(stringLenght.Value);
            }

            if (requiered)
            {
                builder.IsRequired();
            }

            return builder;
        }
    }
}