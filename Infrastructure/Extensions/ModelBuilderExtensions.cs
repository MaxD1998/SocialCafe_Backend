using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SetUser(this ModelBuilder builder)
        {
            builder.SetProperty<UserEntity>(x => x.Email, 50, true);
            builder.SetProperty<UserEntity>(x => x.FirstName, 50, true);
            builder.SetProperty<UserEntity>(x => x.LastName, 50, true);
            builder.SetProperty<UserEntity>(x => x.HashedPassword, requiered: true);
            builder.SetProperty<UserEntity>(x => x.IsDeleted, requiered: true)
                .HasDefaultValue(false);

            builder.Entity<UserEntity>()
                .HasQueryFilter(x => x.IsDeleted == false);
        }

        private static PropertyBuilder SetProperty<T>(this ModelBuilder modelBuilder,
            Expression<Func<T, object>> property,
            int? stringLenght = null,
            bool requiered = false) where T : class
        {
            var builder = modelBuilder.Entity<T>()
                .Property(property);

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