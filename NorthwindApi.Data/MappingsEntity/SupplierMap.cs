using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class SupplierMap : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Ignore(x => x.Version);

            builder.Property(c => c.Id)
               .HasColumnName("Id");

            builder.Property(c => c.CompanyName)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(c => c.ContactName)
               .HasColumnType("varchar(30)")
               .HasMaxLength(30)
               .IsRequired();

            builder.Property(c => c.ContactTitle)
              .HasColumnType("varchar(30)")
              .HasMaxLength(30)
              .IsRequired();

            builder.Property(c => c.Adress)
              .HasColumnType("varchar(60)")
              .HasMaxLength(60)
              .IsRequired();

            builder.Property(c => c.City)
              .HasColumnType("varchar(15)")
              .HasMaxLength(15)
              .IsRequired();

            builder.Property(c => c.Country)
              .HasColumnType("varchar(15)")
              .HasMaxLength(15)
              .IsRequired();

            builder.Property(c => c.Phone)
              .HasColumnType("varchar(24)")
              .HasMaxLength(24)
              .IsRequired();

        }
    }
}
