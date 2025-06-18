using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;  // <-- подключаем Pomelo
using System;

namespace Back.Infrastructure.Persistence
{
    public class IdentityDbContextFactory
        : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var builder = new DbContextOptionsBuilder<IdentityDbContext>();

            // Меняем UseSqlServer на UseMySql
            builder.UseMySql(
                config.GetConnectionString("IdentityConnection"),
                new MySqlServerVersion(new Version(8, 0, 28)) // укажите вашу версию MySQL
            );

            var opts = new OperationalStoreOptions();

            return new IdentityDbContext(
                builder.Options,
                Options.Create(opts));
        }
    }
}
