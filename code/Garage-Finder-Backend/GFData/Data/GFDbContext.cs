using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GFData.Data
{
    public class GFDbContext : DbContext
    {
        public GFDbContext()    
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true); 
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("GarageFinderDB"));
        }

        public virtual DbSet<Categorys>? Category { get; set; }
        public virtual DbSet<Car>? Car { get; set; }
        public virtual DbSet<Feedback>? Feedback { get; set; }
        public virtual DbSet<Garage>? Garage { get; set; }
        public virtual DbSet<Orders>? Orders { get; set; }
        public virtual DbSet<Service>? Service { get; set; }    
        public virtual DbSet<Users>? User { get; set; }
        public virtual DbSet<RoleName>? RoleName { get; set; }
        public virtual DbSet<RefreshToken>? RefreshToken { get; set; }
        public virtual DbSet<FavoriteList>? FavoriteList { get; set; }

        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
        }
    }
}
