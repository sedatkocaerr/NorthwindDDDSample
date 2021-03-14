using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.OrderDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class OrderDetailsMap:IEntityTypeConfiguration<OrderDetail>
    {

        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Ignore(x => x.Version);

            builder.Property(c => c.Id)
               .HasColumnName("Id").ValueGeneratedNever();

            // RelationShip Mappings
            builder.HasOne(x => x.Order)
                  .WithMany(x => x.OrderDetail)
                  .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OrderDetails)
                .IsRequired();

            builder.Property(c => c.UnitPrice)
                .HasColumnType("float")
                .IsRequired();

            builder.Property(c => c.Quantity)
                .HasColumnType("smallint")
                .IsRequired();
            builder.Property(c => c.DisCount)
                .HasColumnType("float")
                .IsRequired();
        }
    }
}
