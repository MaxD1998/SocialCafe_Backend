using Domain.Entity;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CommentConfig : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.SetProperty(x => x.Text, requiered: true);

            builder.Navigation(x => x.User)
                .AutoInclude();
        }
    }
}