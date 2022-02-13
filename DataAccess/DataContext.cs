using Common.Settings;
using DataAccess.Extensions;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionstring = config.GetConnectionString(nameof(ConnectionStrings.DbConnectionString));

            builder.UseSqlServer(connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SetUser();
        }
    }
}