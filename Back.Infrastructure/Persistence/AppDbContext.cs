using Back.Domain.Entities;
using Duende.IdentityServer.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Добавляем DbSet'ы для всех сущностей
        public DbSet<Development> Developments { get; set; }
        public DbSet<CaseStudy> CaseStudies { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Автоматически берёт все конфиги из сборки
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
