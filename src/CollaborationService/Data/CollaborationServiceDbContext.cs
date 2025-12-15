using Microsoft.EntityFrameworkCore;
using CollaborationService.Models.Entities;
using System.Linq.Expressions;

namespace CollaborationService.Data;

public class CollaborationServiceDbContext : DbContext
{
    public CollaborationServiceDbContext(DbContextOptions<CollaborationServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Models.Entities.Task> Tasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<CardAssignment> CardAssignments { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }
    public DbSet<CardComment> CardComments { get; set; }
    public DbSet<CardAttachment> CardAttachments { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("collaborationservice");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollaborationServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<Workspace>().ToTable("workspaces");
        modelBuilder.Entity<Sprint>().ToTable("sprints");
        modelBuilder.Entity<Board>().ToTable("boards");
        modelBuilder.Entity<Column>().ToTable("columns");
        modelBuilder.Entity<Card>().ToTable("cards");
        modelBuilder.Entity<Models.Entities.Task>().ToTable("tasks");
        modelBuilder.Entity<Subtask>().ToTable("subtasks");
        modelBuilder.Entity<CardAssignment>().ToTable("card_assignments");
        modelBuilder.Entity<TaskAssignment>().ToTable("task_assignments");
        modelBuilder.Entity<CardComment>().ToTable("card_comments");
        modelBuilder.Entity<CardAttachment>().ToTable("card_attachments");
        modelBuilder.Entity<ActivityLog>().ToTable("activity_logs");

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