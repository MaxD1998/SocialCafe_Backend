using Domain.Entity;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ConversationMemberConfig : BaseConfig<ConversationMemberEntity>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ConversationMemberEntity> builder)
        {
            builder.SetProperty(x => x.Nick, 50);

            builder.HasIndex(x => new { x.ConversationId, x.UserId })
                .IsUnique();

            builder.Navigation(x => x.User)
                .AutoInclude();
        }
    }
}