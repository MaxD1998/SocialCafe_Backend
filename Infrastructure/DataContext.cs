using ApplicationCore.Helpers;
using ApplicationCore.Settings;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new FriendConfig());
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new UserConfig());
        }
    }
}