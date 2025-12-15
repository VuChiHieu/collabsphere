using Microsoft.EntityFrameworkCore;
using AcademicService.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load .env.local
var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env.local");
if (File.Exists(envPath))
{
    Env.Load(envPath);
}

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

// Get connection string from configuration or environment variable
var connectionString = builder.Configuration.GetConnectionString("AcademicService")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__AcademicService")
    ?? throw new InvalidOperationException("AcademicService connection string not configured");

builder.Services.AddDbContext<AcademicServiceDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
        npgsqlOptions.CommandTimeout(60);
    }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Auto-migrate in development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AcademicServiceDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();