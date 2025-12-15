using Microsoft.EntityFrameworkCore;
using EvaluationService.Data;
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

const string ServiceName = "EvaluationService"; 
const string ConnectionStringEnvVarName = $"ConnectionStrings__{ServiceName}";

// Get connection string from configuration or environment variable
var connectionString = builder.Configuration.GetConnectionString(ServiceName)
    ?? Environment.GetEnvironmentVariable(ConnectionStringEnvVarName)
    ?? throw new InvalidOperationException($"'{ServiceName}' connection string or environment variable '{ConnectionStringEnvVarName}' not configured");

builder.Services.AddDbContext<EvaluationServiceDbContext>(options =>
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
    var dbContext = scope.ServiceProvider.GetRequiredService<EvaluationServiceDbContext>();
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