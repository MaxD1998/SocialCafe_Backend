using Domain.Entity;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MessageConfig : BaseConfig<MessageEntity>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<MessageEntity> builder)
        {
            builder.SetProperty(x => x.Text, requiered: true);

            builder.HasOne(x => x.User)
                .WithMany(x => x.SendMessages)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Converstaion)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ConversationId);

            builder.Navigation(x => x.User)
                .AutoInclude();
        }
    }
}