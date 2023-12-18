using CantinaWebAPI.Domain.OrdersProducts;
using CantinaWebAPI.Domain.Orders;
using CantinaWebAPI.Domain.Products;
using CantinaWebAPI.Domain.Users;
using CantinaWebAPI.EndPoints.Orders;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace CantinaWebAPI.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders {  get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            // User configs
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

        }
    }
}
