using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
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

            builder.Property(c => c.Email)
              .HasColumnType("varchar(100)")
              .HasMaxLength(100)
              .IsRequired();

            builder.Property(c => c.Address)
                .HasColumnType("varchar(60)")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.City)
                .HasColumnType("varchar(15)")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(c => c.PostalCode)
               .HasColumnType("varchar(10)")
               .HasMaxLength(10)
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
