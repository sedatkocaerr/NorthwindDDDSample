using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Ignore(x => x.Version);

            builder.HasMany(x => x.OrderDetails)
               .WithOne(x => x.Product).HasForeignKey(b => b.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Id)
               .HasColumnName("Id");

            builder.Property(c => c.ProductName)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(c => c.SupplierID)
               .HasColumnName("SupplierID")
               .IsRequired();

            builder.Property(c => c.CategoryID)
              .HasColumnName("CategoryID")
              .IsRequired();

            builder.Property(c => c.QuantityPerUnit)
             .HasColumnType("varchar(20)")
             .HasMaxLength(20)
             .IsRequired();

            builder.Property(c => c.UnitPrice)
             .HasColumnType("decimal")
            .IsRequired();

            builder.Property(c => c.UnitsInStock)
            .HasColumnType("float")
           .IsRequired();
        }
    }
}
