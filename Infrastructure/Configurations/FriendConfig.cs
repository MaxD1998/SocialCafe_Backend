using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FriendConfig : IEntityTypeConfiguration<FriendEntity>
    {
        public void Configure(EntityTypeBuilder<FriendEntity> builder)
        {
            builder.HasIndex(x => new { x.InviterId, x.RecipientId })
                .IsUnique();

            builder.Navigation(x => x.Inviter)
                .AutoInclude();
            builder.Navigation(x => x.Recipient)
                .AutoInclude();
        }
    }
}