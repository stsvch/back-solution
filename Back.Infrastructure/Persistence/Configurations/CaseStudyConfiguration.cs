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
    public class CaseStudyConfiguration : IEntityTypeConfiguration<CaseStudy>
    {
        public void Configure(EntityTypeBuilder<CaseStudy> builder)
        {
            builder.ToTable("CaseStudies");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder
                .HasMany(c => c.Photos)
                .WithOne()
                .HasForeignKey("CaseStudyId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
