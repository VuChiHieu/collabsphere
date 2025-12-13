using Microsoft.EntityFrameworkCore;
using NotificationService.Models.Entities;
using System.Linq.Expressions;

namespace NotificationService.Data;

public class NotificationServiceDbContext : DbContext
{
    public NotificationServiceDbContext(DbContextOptions<NotificationServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<EmailNotification> EmailNotifications { get; set; }
    public DbSet<NotificationPreference> NotificationPreferences { get; set; }
    public DbSet<NotificationQueue> NotificationQueues { get; set; }
    public DbSet<UserDeviceToken> UserDeviceTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<Notification>().ToTable("notifications");
        modelBuilder.Entity<EmailNotification>().ToTable("email_notifications");
        modelBuilder.Entity<NotificationPreference>().ToTable("notification_preferences");
        modelBuilder.Entity<NotificationQueue>().ToTable("notification_queues");
        modelBuilder.Entity<UserDeviceToken>().ToTable("user_device_tokens");

        // Apply soft delete filter globally
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(GetSoftDeleteFilter(entityType.ClrType));
            }
        }
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && 
                (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
            
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }

    private static LambdaExpression GetSoftDeleteFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
        var condition = Expression.Equal(property, Expression.Constant(false));
        return Expression.Lambda(condition, parameter);
    }
}