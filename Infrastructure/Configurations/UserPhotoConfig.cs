using Domain.Entities;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserPhotoConfig : BaseConfig<UserPhotoEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserPhotoEntity> builder)
    {
        builder.SetProperty(x => x.ContentType, requiered: true);
        builder.SetProperty(x => x.Data, requiered: true);
        builder.SetProperty(x => x.Name, requiered: true);

        builder.HasIndex(x => new { x.UserId, x.IsMain })
            .IsUnique();
    }
}