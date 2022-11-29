using Domain.Entity;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfig : BaseConfig<UserEntity>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
        {
            builder.SetProperty(x => x.Email, 50, true);
            builder.SetProperty(x => x.FirstName, 50, true);
            builder.SetProperty(x => x.LastName, 50, true);
            builder.SetProperty(x => x.HashedPassword, requiered: true);

            builder.HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}