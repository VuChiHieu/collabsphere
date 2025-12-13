using Microsoft.EntityFrameworkCore;
using TeamService.Models.Entities;
using System.Linq.Expressions;

namespace TeamService.Data;

public class TeamServiceDbContext : DbContext
{
    public TeamServiceDbContext(DbContextOptions<TeamServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TeamMilestone> TeamMilestones { get; set; }
    public DbSet<MilestoneQuestion> MilestoneQuestions { get; set; }
    public DbSet<MilestoneAnswer> MilestoneAnswers { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }
    public DbSet<CheckpointAssignment> CheckpointAssignments { get; set; }
    public DbSet<CheckpointSubmission> CheckpointSubmissions { get; set; }
    public DbSet<TeamProgressLog> TeamProgressLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<Team>().ToTable("teams");
        modelBuilder.Entity<TeamMember>().ToTable("team_members");
        modelBuilder.Entity<TeamMilestone>().ToTable("team_milestones");
        modelBuilder.Entity<MilestoneQuestion>().ToTable("milestone_questions");
        modelBuilder.Entity<MilestoneAnswer>().ToTable("milestone_answers");
        modelBuilder.Entity<Checkpoint>().ToTable("checkpoints");
        modelBuilder.Entity<CheckpointAssignment>().ToTable("checkpoint_assignments");
        modelBuilder.Entity<CheckpointSubmission>().ToTable("checkpoint_submissions");
        modelBuilder.Entity<TeamProgressLog>().ToTable("team_progress_logs");

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