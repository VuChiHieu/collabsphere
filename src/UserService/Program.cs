using Microsoft.EntityFrameworkCore;
using UserService.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load .env.local from project root
var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env.local");
if (File.Exists(envPath))
{
    Env.Load(envPath);
}

// Build connection string from environment variables with fallbacks
var host = Environment.GetEnvironmentVariable("USER_SERVICE_DB_HOST") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_HOST") 
    ?? "localhost";
    
var port = Environment.GetEnvironmentVariable("USER_SERVICE_DB_PORT") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_PORT") 
    ?? "5432";
    
var database = Environment.GetEnvironmentVariable("USER_SERVICE_DB_NAME") 
    ?? "cosre_userservice_local";
    
var username = Environment.GetEnvironmentVariable("USER_SERVICE_DB_USER") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_USER") 
    ?? "cosre_admin";
    
var password = Environment.GetEnvironmentVariable("USER_SERVICE_DB_PASSWORD") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_PASSWORD")
    ?? throw new InvalidOperationException("Database password not configured");

// Use Development config for local, build connection string for production
var connectionString = builder.Environment.IsDevelopment() 
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";

// Add DbContext
builder.Services.AddDbContext(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
        npgsqlOptions.CommandTimeout(60);
    }));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Auto-migrate database on startup (Development only)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();