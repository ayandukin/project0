using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArmoryDb.DataAccess.Entities
{
    public partial class ArmoryDbContext : DbContext
    {
        public ArmoryDbContext()
        {
        }

        public ArmoryDbContext(DbContextOptions<ArmoryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "arm");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.InvId)
                    .HasName("PK__Inventor__9DC82C4A05D7CE82");

                entity.ToTable("Inventory", "arm");

                entity.Property(e => e.InvId).HasColumnName("InvID");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Item__05D8E0BE");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Locat__06CD04F7");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice", "arm");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__Item__7A672E12");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__OrderID__797309D9");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Item__737584F72AF64455");

                entity.ToTable("Item", "arm");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Location__737584F7E852686F");

                entity.ToTable("Location", "arm");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BAFFF777C45");

                entity.ToTable("Orders", "arm");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PurchaseDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Customer__75A278F5");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Location__74AE54BC");
            });
        }
    }
}
