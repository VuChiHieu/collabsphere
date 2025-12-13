using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==================== ADD OCELOT CONFIGURATION ====================
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// ==================== ADD AUTHENTICATION ====================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? "YourSuperSecretKeyForCollabSphere2024!@#";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"] ?? "CollabSphere",
            ValidAudience = jwtSettings["Audience"] ?? "CollabSphereUsers",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            ),
            ClockSkew = TimeSpan.Zero
        };
    });

// ==================== ADD CORS ====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ==================== ADD HEALTH CHECKS ====================
builder.Services.AddHealthChecks();

// ==================== ADD LOGGING ====================
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// ==================== CONFIGURE MIDDLEWARE ====================
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

// ==================== MAP ENDPOINTS FOR GATEWAY ITSELF ====================
// Chỉ xử lý root path và /health, còn lại để Ocelot handle
app.MapWhen(
    context => context.Request.Path == "/" || context.Request.Path == "/health",
    appBuilder =>
    {
        appBuilder.UseRouting();
        appBuilder.UseEndpoints(endpoints =>
        {
            // Root endpoint
            endpoints.MapGet("/", () => Results.Ok(new
            {
                service = "CollabSphere API Gateway",
                version = "1.0.0",
                status = "Running",
                timestamp = DateTime.UtcNow,
                routes = new[]
                {
                    "/api/auth/*",
                    "/api/users/*",
                    "/api/classes/*",
                    "/api/projects/*",
                    "/api/teams/*"
                }
            }));

            // Health check
            endpoints.MapHealthChecks("/health");
        });
    }
);

// ==================== OCELOT (Handle tất cả /api/* requests) ====================
await app.UseOcelot();

app.Run();