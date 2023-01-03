using Domain.Entity;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CommentConfig : BaseConfig<CommentEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.SetProperty(x => x.Text, requiered: true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Navigation(x => x.User)
            .AutoInclude();
    }
}