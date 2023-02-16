using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Bases;

public abstract class BaseConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableNameHelper.Convert(typeof(TEntity).Name));
        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}