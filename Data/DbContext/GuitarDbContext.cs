using GuitarWorkshop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuitarWorkshop.Data.DbContext
{
    public class GuitarDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Guitar> Guitars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RepairService> RepairServices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=GuitarWorkshopDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Guitar)
                .WithMany(g => g.Orders)
                .HasForeignKey(o => o.GuitarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.RepairService)
                .WithMany()
                .HasForeignKey(o => o.RepairServiceId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}