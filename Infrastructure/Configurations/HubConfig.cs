using Domain.Entities;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class HubConfig : BaseConfig<HubEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<HubEntity> builder)
    {
        builder.SetProperty(x => x.ConnectionId, requiered: true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Hubs)
            .HasForeignKey(x => x.UserId);

        builder.HasIndex(x => new { x.Type, x.UserId })
            .IsUnique();
    }
}