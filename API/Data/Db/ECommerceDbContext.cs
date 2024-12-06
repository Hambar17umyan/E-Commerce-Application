using API.Models.Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Data.Db
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Cart> Carts { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Cart>(cb =>
            {
                cb.HasKey(c => c.Id);
                cb.Property(c => c.Id).ValueGeneratedOnAdd();

                cb.HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<CartItem>(cib =>
            {
                cib.HasKey(cib => cib.Id);
                cib.Property(ci => ci.Id).ValueGeneratedOnAdd();

                cib.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<Inventory>(ib =>
            {
                ib.HasKey(ib => ib.Id);
                ib.Property(i => i.Id).ValueGeneratedOnAdd();

                ib.HasOne(ib => ib.Product)
                .WithMany()
                .HasForeignKey(ib => ib.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<LineItem>(lib =>
            {
                lib.HasKey(li => li.Id);
                lib.Property(li => li.Id).ValueGeneratedOnAdd();

                lib.HasOne(li => li.Order)
                .WithMany(o => o.LineItems)
                .HasForeignKey(li => li.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                lib.HasOne(li => li.Product)
                .WithMany()
                .HasForeignKey(li => li.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            mb.Entity<Order>(ob =>
            {
                ob.HasKey(o => o.Id);
                ob.Property(o => o.Id).ValueGeneratedOnAdd();

                ob.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<Product>(pb =>
            {
                pb.HasKey(p => p.Id);
                pb.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            mb.Entity<Role>(rb =>
            {
                rb.HasKey(r => r.Id);
                rb.Property(r => r.Id).ValueGeneratedOnAdd();
            });

            mb.Entity<User>(ub =>
            {
                ub.HasKey(u => u.Id);
                ub.Property(u => u.Id).ValueGeneratedOnAdd();

                ub.HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    j => j.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId"),

                    j => j.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                );

                ub.HasOne(u => u.Cart)
                .WithOne()
                .HasForeignKey<Cart>(c => c.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(mb);
        }
    }
}
