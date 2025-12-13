using Microsoft.EntityFrameworkCore;
using FileService.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env.local");
if (File.Exists(envPath))
{
    Env.Load(envPath);
}

var host = Environment.GetEnvironmentVariable("FILE_SERVICE_DB_HOST") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_HOST") 
    ?? "localhost";
    
var port = Environment.GetEnvironmentVariable("FILE_SERVICE_DB_PORT") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_PORT") 
    ?? "5432";
    
var database = Environment.GetEnvironmentVariable("FILE_SERVICE_DB_NAME") 
    ?? "cosre_fileservice_local";
    
var username = Environment.GetEnvironmentVariable("FILE_SERVICE_DB_USER") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_USER") 
    ?? "cosre_admin";
    
var password = Environment.GetEnvironmentVariable("FILE_SERVICE_DB_PASSWORD") 
    ?? Environment.GetEnvironmentVariable("RENDER_DB_PASSWORD")
    ?? throw new InvalidOperationException("Database password not configured");

var connectionString = builder.Environment.IsDevelopment() 
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";

builder.Services.AddDbContext<FileServiceDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
        npgsqlOptions.CommandTimeout(60);
    }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FileServiceDbContext>();
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