using Domain.Entity;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ConversationConfig : BaseConfig<ConversationEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ConversationEntity> builder)
    {
        builder.SetProperty(x => x.Name, 50);

        builder.Navigation(x => x.ConversationMembers)
            .AutoInclude();
    }
}