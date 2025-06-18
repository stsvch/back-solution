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
    public class DevelopmentConfiguration : IEntityTypeConfiguration<Development>
    {
        public void Configure(EntityTypeBuilder<Development> builder)
        {
            builder.ToTable("Developments");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(2000);

            // связь «один Development — много Photos»
            builder
                .HasMany(d => d.Photos)
                .WithOne()            // Photo не имеет навигации на родителя
                .HasForeignKey("DevelopmentId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
