using Domain.Entity;
using Infrastructure.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class FriendConfig : BaseConfig<FriendEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<FriendEntity> builder)
    {
        builder.HasOne(x => x.Inviter)
            .WithMany(x => x.InvitedUsers)
            .HasForeignKey(x => x.InviterId);

        builder.HasOne(x => x.Recipient)
            .WithMany(x => x.InvitedByUsers)
            .HasForeignKey(x => x.RecipientId);

        builder.HasIndex(x => new { x.InviterId, x.RecipientId })
            .IsUnique();
    }
}