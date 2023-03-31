using ApplicationCore.Helpers;
using ApplicationCore.Settings;
using Domain.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure;

public class DataContext : DbContext
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
                ((BaseEntity)entity.Entity).CreateTime = DateTime.UtcNow;

            if (entity.State == EntityState.Modified)
                ((BaseEntity)entity.Entity).ModifyTime = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var config = ConfigHelper.SetConfings();
        var connectionstring = config.GetConnectionString(nameof(ConnectionStrings.DbConnectionString));

        builder.UseNpgsql(connectionstring);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}