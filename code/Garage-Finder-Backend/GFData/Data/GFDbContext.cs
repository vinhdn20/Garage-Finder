using GFData.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Data
{
    public class GFDbContext : DbContext
    {
        public GFDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("GarageFinderDB"));
        }
        public virtual DbSet<Car>? Cars { get; set; }
        public virtual DbSet<Feedback>? Feedbacks { get; set; }
        public virtual DbSet<Garage>? Garages { get; set; }
        public virtual DbSet<OrderDetail>? OrderDetails { get; set; }
        public virtual DbSet<Orders>? Orders { get; set; }
        public virtual DbSet<Service>? Services { get; set; }
        public virtual DbSet<Users>? Users { get; set; }
        public virtual DbSet<RoleName>? RoleName { get; set; }

    }
}
