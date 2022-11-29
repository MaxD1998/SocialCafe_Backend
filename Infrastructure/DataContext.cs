using ApplicationCore.Helpers;
using ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
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
}