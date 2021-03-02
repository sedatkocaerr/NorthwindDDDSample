using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindApi.Domain.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Data.MappingsEntity
{
    public class CategoryMap :  IEntityTypeConfiguration<Category>
    {
        
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Ignore(x => x.Version);

            builder.Property(c => c.Id)
               .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(15)")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.Picture)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);
                
        }
    }
}
