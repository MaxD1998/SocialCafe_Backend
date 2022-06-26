using Domain.Entity;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.OwnsMany(
                x => x.RefreshTokens,
                prop =>
                {
                    prop.SetProperty(x => x.CreationDate, requiered: true);
                    prop.SetProperty(x => x.ExpireDate, requiered: true);
                    prop.SetProperty(x => x.Token, requiered: true);
                });
            builder.SetProperty(x => x.Email, 50, true);
            builder.SetProperty(x => x.FirstName, 50, true);
            builder.SetProperty(x => x.LastName, 50, true);
            builder.SetProperty(x => x.HashedPassword, requiered: true);

            builder.HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}