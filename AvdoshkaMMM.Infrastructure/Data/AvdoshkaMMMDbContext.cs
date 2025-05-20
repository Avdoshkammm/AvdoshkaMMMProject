using AvdoshkaMMM.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AvdoshkaMMM.Infrastructure.Data
{
    public class AvdoshkaMMMDbContext : IdentityDbContext<User>
    {
        public AvdoshkaMMMDbContext(DbContextOptions<AvdoshkaMMMDbContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderProduct>()
                .HasOne(o => o.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(oid => oid.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(pid => pid.ProductID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>()
                .HasOne(u => u.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(uid => uid.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
