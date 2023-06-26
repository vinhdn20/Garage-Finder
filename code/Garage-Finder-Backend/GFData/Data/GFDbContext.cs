using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

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


        public virtual DbSet<Brand>? Brand { get; set; }
        public virtual DbSet<Categorys>? Category { get; set; }
        public virtual DbSet<Car>? Car { get; set; }
        public virtual DbSet<Feedback>? Feedback { get; set; }
        public virtual DbSet<Garage>? Garage { get; set; }
        public virtual DbSet<GarageBrand>? GarageBrand { get; set; }
        public virtual DbSet<Orders>? Orders { get; set; }
        public virtual DbSet<Service>? Service { get; set; }    
        public virtual DbSet<Users>? User { get; set; }
        public virtual DbSet<RoleName>? RoleName { get; set; }
        public virtual DbSet<RefreshToken>? RefreshToken { get; set; }
        public virtual DbSet<FavoriteList>? FavoriteList { get; set; }
        public virtual DbSet<ImageOrders>? ImageOrders { get; set; }
        public virtual DbSet<FileOrders>? FileOrders { get; set; }
        public virtual DbSet<CategoryGarage>? CategoryGarage { get; set; }
        public virtual DbSet<ImageGarage>? ImageGarage { get; set; }
        public virtual DbSet<GarageInfo>? GarageInfo { get; set; }  

        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            base.OnModelCreating(optionsBuilder);
            optionsBuilder.Entity<FavoriteList>()
            .HasOne(p => p.Garage)
            .WithMany(u => u.FavoriteList)
            .HasForeignKey(c => c.GarageID)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<Feedback>()
            .HasOne(p => p.User)
            .WithMany(u => u.Feedbacks)
            .HasForeignKey(c => c.UserID)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<Orders>()
            .HasOne(p => p.Car)
            .WithMany(u => u.Orders)
            .HasForeignKey(c => c.CarID)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<Orders>()
            .HasOne(p => p.Garage)
            .WithMany(u => u.Orders)
            .HasForeignKey(c => c.GarageID)
            .OnDelete(DeleteBehavior.NoAction);

            //optionsBuilder.Entity<Orders>()
            //.HasOne(p => p.Service)
            //.WithMany(u => u.Orders)
            //.HasForeignKey(c => c.ServiceID)
            //.OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<GarageInfo>()
            .HasOne(p => p.User)
            .WithMany(u => u.GarageInfos)
            .HasForeignKey(c => c.UserID)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<Service>().Property(p => p.ServiceID).ValueGeneratedOnAdd()
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
