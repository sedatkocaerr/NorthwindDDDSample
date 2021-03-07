using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Ignore(x => x.Version);

            builder.Property(c => c.Id)
               .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Surname)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.PasswordHash)
               .HasColumnType("varbinary");

            builder.Property(c => c.PasswordSalt)
               .HasColumnType("varbinary");

            builder.Property(c => c.RegisterDate)
              .HasColumnType("Date")
              .HasMaxLength(100);
        }
    }
}
