using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Ignore(x => x.Version);
            // RelationShip Mappings
            builder.HasOne(x => x.Customer)
                  .WithMany(x => x.Orders)
                  .IsRequired();

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Orders)
                .IsRequired();

            builder.HasMany(x => x.OrderDetail)
               .WithOne(x => x.Order).HasForeignKey(b => b.OrderID)
            .OnDelete(DeleteBehavior.Cascade);


            builder.Property(c => c.Id)
               .HasColumnName("Id").ValueGeneratedNever();

            builder.Property(c => c.CustomerID)
                .IsRequired();

            builder.Property(c => c.EmployeeID)
                .IsRequired();

            builder.Property(c => c.OrderDate)
                .HasColumnType("Date")
                .IsRequired();

            builder.Property(c => c.RequiredDate)
                 .HasColumnType("Date")
                .IsRequired();

            builder.Property(c => c.ShippedDate)
                .HasColumnType("Date")
                .IsRequired();

            builder.Property(c => c.ShipName)
               .HasColumnType("varchar(60)")
               .HasMaxLength(60)
               .IsRequired();

            builder.Property(c => c.ShipVia)
                .IsRequired();

            builder.Property(c => c.ShipAddress)
               .HasColumnType("varchar(65)")
               .HasMaxLength(65)
               .IsRequired();

            builder.Property(c => c.ShipCity)
               .HasColumnType("varchar(15)")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(c => c.ShipPostalCode)
               .HasColumnType("varchar(10)")
               .HasMaxLength(10);


            builder.Property(c => c.ShipCountry)
               .HasColumnType("varchar(15)")
               .HasMaxLength(15)
               .IsRequired();
        }
    }
}
