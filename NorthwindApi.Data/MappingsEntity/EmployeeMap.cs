using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Ignore(x => x.Version);
            builder.Property(c => c.Id)
               .HasColumnName("Id");

            builder.Property(c => c.FirstName)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.Title)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.BirthDate)
                .HasColumnType("Date")
                .IsRequired();

            builder.Property(c => c.HireDate)
                .HasColumnType("Date")
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

        }
    }
}
