using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;

namespace DataAccess;

public class OrderDbContext : DbContext
{
    private readonly DbConnection _connection;

    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
        _connection = RelationalOptionsExtension.Extract(options).Connection!;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
                                     {
                                         entity.ToTable("Addresses");

                                         entity.HasKey(e => e.AddressId);

                                         entity.Property(e => e.AddressId)
                                               .ValueGeneratedOnAdd()
                                               .UseIdentityColumn(1);

                                         entity.Property(e => e.Created)
                                               .HasColumnType("datetime")
                                               .IsRequired();

                                         entity.Property(e => e.Hash)
                                               .IsRequired()
                                               .HasMaxLength(50)
                                               .IsUnicode(false);
                                         entity.Property(e => e.LineOne)
                                               .IsRequired()
                                               .HasMaxLength(255)
                                               .IsUnicode(false);
                                         entity.Property(e => e.LineTwo)
                                               .IsRequired()
                                               .HasMaxLength(255)
                                               .IsUnicode(false);
                                         entity.Property(e => e.LineThree)
                                               .IsRequired()
                                               .HasMaxLength(255)
                                               .IsUnicode(false);
                                         entity.Property(e => e.PostCode)
                                               .IsRequired()
                                               .HasMaxLength(10)
                                               .IsUnicode(false);
                                     });

        modelBuilder.Entity<Customer>(entity =>
                                      {
                                          entity.ToTable("Customers");

                                          entity.HasKey(e => e.CustomerId);

                                          entity.Property(e => e.CustomerId)
                                                .ValueGeneratedOnAdd()
                                                .UseIdentityColumn(1);

                                          entity.Property(e => e.Created).HasColumnType("datetime");
                                          entity.Property(e => e.Email)
                                                .IsRequired()
                                                .HasMaxLength(255)
                                                .IsUnicode(false);
                                          entity.Property(e => e.ExternalId)
                                                .IsRequired()
                                                .HasMaxLength(50)
                                                .IsUnicode(false);
                                          entity.Property(e => e.Name)
                                                .IsRequired()
                                                .HasMaxLength(255)
                                                .IsUnicode(false);
                                          entity.Property(e => e.PhoneNumber)
                                                .IsRequired()
                                                .HasMaxLength(12)
                                                .IsUnicode(false);
                                      });

        modelBuilder.Entity<Order>(entity =>
                                   {
                                       entity.ToTable("Orders");

                                       entity.HasKey(e => e.OrderId);

                                       entity.Property(e => e.OrderId)
                                             .ValueGeneratedOnAdd()
                                             .UseIdentityColumn(1);

                                       entity.Property(e => e.Created)
                                             .IsRequired()
                                             .HasColumnType("datetime");
                                       entity.Property(e => e.LastModified)
                                             .IsRequired()
                                             .HasColumnType("datetime");

                                       entity.Property(e => e.OrderNumber)
                                             .IsRequired()
                                             .HasMaxLength(50)
                                             .IsUnicode(false);

                                       entity.Property(e => e.TotalPrice)
                                             .IsRequired()
                                             .HasColumnType("money");

                                       entity.HasOne(d => d.BillingAddress).WithMany(p => p.BillingOrders)
                                             .HasForeignKey(d => d.BillingAddressId)
                                             .OnDelete(DeleteBehavior.ClientSetNull)
                                             .HasConstraintName("FK_Orders_Addresses_Billing");

                                       entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                                             .HasForeignKey(d => d.CustomerId)
                                             .OnDelete(DeleteBehavior.ClientSetNull)
                                             .HasConstraintName("FK_Orders_Customers");

                                       entity.HasOne(d => d.ShippingAddress).WithMany(p => p.ShippingOrders)
                                             .HasForeignKey(d => d.ShippingAddressId)
                                             .OnDelete(DeleteBehavior.ClientSetNull)
                                             .HasConstraintName("FK_Orders_Addresses_Shipping");
                                   });

        modelBuilder.Entity<OrderItem>(entity =>
                                       {
                                           entity.ToTable("OrderItems");

                                           entity.HasKey(e => e.OrderItemId);

                                           entity.Property(e => e.OrderItemId)
                                                 .ValueGeneratedOnAdd()
                                                 .UseIdentityColumn(1);

                                           entity.Property(e => e.Quantity)
                                                 .IsRequired();

                                           entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                                                 .HasForeignKey(d => d.OrderId)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_OrderItems_Orders");

                                           entity.HasOne(d => d.Variant).WithMany(p => p.OrderItems)
                                                 .HasForeignKey(d => d.VariantId)
                                                 .OnDelete(DeleteBehavior.ClientSetNull)
                                                 .HasConstraintName("FK_OrderItems_Variants");
                                       });

        modelBuilder.Entity<OutboxMessage>(entity =>
                                           {
                                               entity.ToTable("Outbox");

                                               entity.HasKey(e => e.OutboxMessageId);

                                               entity.Property(e => e.OutboxMessageId)
                                                     .ValueGeneratedOnAdd()
                                                     .UseIdentityColumn(1);

                                               entity.Property(e => e.Created)
                                                     .IsRequired()
                                                     .HasColumnType("datetime");

                                               entity.Property(e => e.MessageId)
                                                     .IsRequired()
                                                     .HasMaxLength(50)
                                                     .IsUnicode(false);

                                               entity.Property(e => e.Payload)
                                                     .IsRequired()
                                                     .IsUnicode(false);
                                           });

        modelBuilder.Entity<Product>(entity =>
                                     {
                                         entity.ToTable("Products");

                                         entity.HasKey(e => e.ProductId);

                                         entity.Property(e => e.ProductId)
                                               .ValueGeneratedOnAdd()
                                               .UseIdentityColumn(1);

                                         entity.Property(e => e.ExternalId)
                                               .IsRequired()
                                               .HasMaxLength(50)
                                               .IsUnicode(false);

                                         entity.Property(e => e.ImageUrl)
                                               .IsRequired()
                                               .HasMaxLength(1000)
                                               .IsUnicode(false);

                                         entity.Property(e => e.Name)
                                               .IsRequired()
                                               .HasMaxLength(255)
                                               .IsUnicode(false);
                                     });

        modelBuilder.Entity<Variant>(entity =>
                                     {
                                         entity.ToTable("Variants");

                                         entity.HasKey(e => e.VariantId);

                                         entity.Property(e => e.VariantId)
                                               .ValueGeneratedOnAdd()
                                               .UseIdentityColumn(1);

                                         entity.Property(e => e.ExternalId)
                                               .IsRequired()
                                               .HasMaxLength(50)
                                               .IsUnicode(false);

                                         entity.Property(e => e.Name)
                                               .IsRequired()
                                               .HasMaxLength(50)
                                               .IsUnicode(false);

                                         entity.Property(e => e.Price)
                                               .IsRequired()
                                               .HasColumnType("money");

                                         entity.Property(e => e.Sku)
                                               .IsRequired()
                                               .HasMaxLength(50)
                                               .IsUnicode(false);

                                         entity.HasOne(d => d.Product).WithMany(p => p.Variants)
                                               .HasForeignKey(d => d.ProductId)
                                               .OnDelete(DeleteBehavior.ClientSetNull)
                                               .HasConstraintName("FK_Variants_Products");
                                     });
    }

    public override void Dispose()
    {
        _connection.Dispose();

        base.Dispose();
    }
}

