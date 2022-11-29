using Domain.Entity;
using Infrastructure.Bases;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PostConfig : BaseConfig<PostEntity>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PostEntity> builder)
        {
            builder.SetProperty(x => x.Text, requiered: true);

            builder.Navigation(x => x.User)
                .AutoInclude();
        }
    }
}