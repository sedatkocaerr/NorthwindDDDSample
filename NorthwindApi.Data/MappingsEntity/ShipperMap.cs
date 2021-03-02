using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Shippers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class ShipperMap : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.Ignore(x => x.Version);

            builder.HasMany(x => x.Orders)
               .WithOne(x => x.Shipper).HasForeignKey(b => b.ShipVia)
            .OnDelete(DeleteBehavior.Cascade);
              

            builder.Property(c => c.Id)
               .HasColumnName("Id");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.CompanyName)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(c => c.Phone)
               .HasColumnType("varchar(24)")
               .HasMaxLength(24)
               .IsRequired();

            
        }
    }
}
