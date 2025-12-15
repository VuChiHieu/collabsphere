using Microsoft.EntityFrameworkCore;
using UserService.Data;
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

const string ServiceName = "UserService"; 
const string ConnectionStringEnvVarName = $"ConnectionStrings__{ServiceName}";

// Get connection string from configuration or environment variable
var connectionString = builder.Configuration.GetConnectionString(ServiceName)
    ?? Environment.GetEnvironmentVariable(ConnectionStringEnvVarName)
    ?? throw new InvalidOperationException($"'{ServiceName}' connection string or environment variable '{ConnectionStringEnvVarName}' not configured");

// Add DbContext
builder.Services.AddDbContext<UserServiceDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
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
    var dbContext = scope.ServiceProvider.GetRequiredService<UserServiceDbContext>();
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