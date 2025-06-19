using Back.Application.Interfaces;
using Back.Domain.Repositories;
using Back.Infrastructure.Identity;
using Back.Infrastructure.Persistence;
using Back.Infrastructure.Repositories;
using Back.Infrastructure.Services;
using Back.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 28))   // укажите вашу версию MySQL
                )
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information)
            );

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("IdentityConnection"),
                    new MySqlServerVersion(new Version(8, 0, 28))
                )
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>()!;

            var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
            var issuer = jwtSettings.Issuer;
            var audience = jwtSettings.Audience;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateLifetime = true
                };
            });

            services.AddAuthorization();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IContentRepository<>), typeof(ContentRepository<>));

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddSingleton<IFileStorageService, FileStorageService>();


            return services;
        }
    }
}
