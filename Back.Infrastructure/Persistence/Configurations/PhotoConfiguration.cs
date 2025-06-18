using Back.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Infrastructure.Persistence.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Path)
                .IsRequired()
                .HasMaxLength(500);

            // Каждый Photo связан ровно с одним родителем через внешний ключ
            // Настроим связь в конфигурации дочернего класса ниже
        }
    }
}
