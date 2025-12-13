var builder = WebApplication.CreateBuilder(args);

// Minimal setup - no external packages needed
builder.Services.AddHealthChecks();

var app = builder.Build();

// Health check endpoint
app.MapHealthChecks("/health");

// Test endpoints
app.MapGet("/api/auth/test", () => Results.Ok(new
{
    service = "AuthService",
    status = "Running",
    port = 5002,
    timestamp = DateTime.UtcNow
}));

app.MapGet("/", () => "AuthService is running!");

Console.WriteLine("AuthService starting on http://localhost:5002");
app.Run();