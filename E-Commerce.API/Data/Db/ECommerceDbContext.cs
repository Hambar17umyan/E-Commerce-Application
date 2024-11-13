using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace E_Commerce.API.Data.Db
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<LineItem> LineItems { get; set; }
        //public DbSet<Inventory> Inventories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ub =>
            {
                ub.HasKey(u => u.Id);
                ub.Property(u => u.Id).ValueGeneratedOnAdd();

                ub.HasMany(u => u.Roles).
                WithMany();

                ub.HasMany(u => u.Orders).
                WithOne(o => o.User).
                HasForeignKey(o => o.UserId).
                OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Role>(rb =>
            {
                rb.HasKey(r => r.Id);
            });
            modelBuilder.Entity<Order>(ob =>
            {
                ob.HasKey(o => o.Id);
                ob.HasMany(o => o.LineItems)
                .WithOne(li => li.Order)
                .HasForeignKey(li => li.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Product>(pb =>
            {
                pb.HasKey(p => p.Id);
            });
            modelBuilder.Entity<LineItem>(lib =>
            {
                lib.HasKey(li => li.Id);
                lib.HasOne(li => li.Product)
                .WithMany()
                .HasForeignKey(li => li.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Inventory>(ib =>
            {
                ib.HasKey(i => i.Id);
                ib.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
