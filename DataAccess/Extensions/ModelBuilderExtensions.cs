using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SetRefreshToken(this ModelBuilder builder)
        {
            builder.SetProperty<RefreshToken>(x => x.CreationDate, requiered: true);
            builder.SetProperty<RefreshToken>(x => x.ExpireDate, requiered: true);
            builder.SetProperty<RefreshToken>(x => x.Token, requiered: true);
        }

        public static void SetUser(this ModelBuilder builder)
        {
            builder.SetProperty<User>(x => x.Email, 50, true);
            builder.SetProperty<User>(x => x.FirstName, 50, true);
            builder.SetProperty<User>(x => x.LastName, 50, true);
            builder.SetProperty<User>(x => x.HashPassword, requiered: true);
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