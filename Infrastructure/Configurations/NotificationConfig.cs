using Domain.Entities;
using Infrastructure.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class NotificationConfig : BaseConfig<NotificationEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.HasIndex(x => new { x.UserId, x.Type, x.RecipientId })
            .IsUnique();

        builder.HasOne(x => x.Recipient)
            .WithMany(x => x.NotificationsRecipient)
            .HasForeignKey(x => x.RecipientId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.UserId);

        builder.Navigation(x => x.User)
            .AutoInclude();
    }
}