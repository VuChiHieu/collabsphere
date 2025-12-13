using Microsoft.EntityFrameworkCore;
using ProjectService.Models.Entities;
using System.Linq.Expressions;

namespace ProjectService.Data;

public class ProjectServiceDbContext : DbContext
{
    public ProjectServiceDbContext(DbContextOptions<ProjectServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectObjective> ProjectObjectives { get; set; }
    public DbSet<ProjectMilestone> ProjectMilestones { get; set; }
    public DbSet<ClassProject> ClassProjects { get; set; }
    public DbSet<ProjectAIGeneration> ProjectAIGenerations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<Project>().ToTable("projects");
        modelBuilder.Entity<ProjectObjective>().ToTable("project_objectives");
        modelBuilder.Entity<ProjectMilestone>().ToTable("project_milestones");
        modelBuilder.Entity<ClassProject>().ToTable("class_projects");
        modelBuilder.Entity<ProjectAIGeneration>().ToTable("project_ai_generations");

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