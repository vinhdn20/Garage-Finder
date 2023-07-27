using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("GarageFinderDB"),
                sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
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
        public virtual DbSet<GuestOrder>? GuestOrder { get; set; }
        public virtual DbSet<ImageGuestOrder>? ImageGuestOrders { get; set; }
        public virtual DbSet<FileGuestOrders>? FileGuestOrders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<GuestOrderDetail> GuestOrderDetail { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffRefreshToken> StaffRefreshToken { get; set; }
        public virtual DbSet<StaffNotification> StaffNotification { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<RoomChat> RoomChat { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<StaffMessage> StaffMessages { get; set; }
        public virtual DbSet<Subscribe> Subscribes { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<MessageToUser> MessageToUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder optionsBuilder)
        {
            base.OnModelCreating(optionsBuilder);
            optionsBuilder.Entity<FavoriteList>()
            .HasOne(p => p.Garage)
            .WithMany(u => u.FavoriteList)
            .HasForeignKey(c => c.GarageID)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<Message>()
            .HasOne(p => p.Users)
            .WithMany(u => u.Messages)
            .HasForeignKey(c => c.UserID)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<StaffMessage>()
            .HasOne(p => p.Staff)
            .WithMany(u => u.StaffMessages)
            .HasForeignKey(c => c.StaffId)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<MessageToUser>()
            .HasOne(p => p.Sender)
            .WithMany(u => u.MessageToUser)
            .HasForeignKey(c => c.SenderUserID)
            .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<RoomChat>()
            .HasOne(p => p.Users)
            .WithMany(u => u.RoomChat)
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

            optionsBuilder.Entity<GuestOrderDetail>()
           .HasOne(p => p.CategoryGarage)
           .WithMany(u => u.GuestOrderDetails)
           .HasForeignKey(c => c.CategoryGarageID)
           .OnDelete(DeleteBehavior.NoAction);
            
            optionsBuilder.Entity<OrderDetail>()
           .HasOne(p => p.CategoryGarage)
           .WithMany(u => u.OrderDetails)
           .HasForeignKey(c => c.CategoryGarageID)
           .OnDelete(DeleteBehavior.NoAction);

            optionsBuilder.Entity<Service>().Property(p => p.ServiceID).ValueGeneratedOnAdd()
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
