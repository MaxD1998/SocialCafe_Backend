using Domain.Entity;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RefreshTokenConfig : BaseConfig<RefreshTokenEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.SetProperty(x => x.CreationDate, requiered: true);
        builder.SetProperty(x => x.ExpireDate, requiered: true);
        builder.SetProperty(x => x.Token, requiered: true);

        builder.HasIndex(x => x.Token)
            .IsUnique();

        builder.Navigation(x => x.User)
            .AutoInclude();
    }
}