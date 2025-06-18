using Back.Application.Extensions;
using Back.Infrastructure.Extensions;
using Back.Infrastructure.Persistence;
using Back.WebApi;
using Back.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RegisteredUser", policy =>
        policy.RequireAuthenticatedUser());

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(opts =>
  opts.AddDefaultPolicy(p =>
    p.WithOrigins("http://localhost:3000")
     .AllowAnyHeader()
     .AllowAnyMethod()
     .AllowCredentials()
  ));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    var identityDb = services.GetRequiredService<IdentityDbContext>();
    identityDb.Database.Migrate();

    AdminSeeder.SeedAsync(services).GetAwaiter().GetResult();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// В конце сборки приложения, перед MapControllers / Run():
var env = builder.Environment;

// убедиться, что папка существует
var uploadsDir = Path.Combine(env.ContentRootPath, "Uploads");
if (!Directory.Exists(uploadsDir))
    Directory.CreateDirectory(uploadsDir);

// раздаём файлы из /Uploads по адресу /uploads
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsDir),
    RequestPath = "/uploads"
});

app.MapControllers();

app.Run();
