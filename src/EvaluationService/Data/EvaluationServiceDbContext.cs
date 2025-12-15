using Microsoft.EntityFrameworkCore;
using EvaluationService.Models.Entities;
using System.Linq.Expressions;

namespace EvaluationService.Data;

public class EvaluationServiceDbContext : DbContext
{
    public EvaluationServiceDbContext(DbContextOptions<EvaluationServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<TeamEvaluation> TeamEvaluations { get; set; }
    public DbSet<MemberEvaluation> MemberEvaluations { get; set; }
    public DbSet<MilestoneAnswerEvaluation> MilestoneAnswerEvaluations { get; set; }
    public DbSet<CheckpointEvaluation> CheckpointEvaluations { get; set; }
    public DbSet<PeerReview> PeerReviews { get; set; }
    public DbSet<MilestoneAnswerPeerReview> MilestoneAnswerPeerReviews { get; set; }
    public DbSet<EvaluationTemplate> EvaluationTemplates { get; set; }
    public DbSet<EvaluationCriterion> EvaluationCriteria { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("evaluationservice");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EvaluationServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<TeamEvaluation>().ToTable("team_evaluations");
        modelBuilder.Entity<MemberEvaluation>().ToTable("member_evaluations");
        modelBuilder.Entity<MilestoneAnswerEvaluation>().ToTable("milestone_answer_evaluations");
        modelBuilder.Entity<CheckpointEvaluation>().ToTable("checkpoint_evaluations");
        modelBuilder.Entity<PeerReview>().ToTable("peer_reviews");
        modelBuilder.Entity<MilestoneAnswerPeerReview>().ToTable("milestone_answer_peer_reviews");
        modelBuilder.Entity<EvaluationTemplate>().ToTable("evaluation_templates");
        modelBuilder.Entity<EvaluationCriterion>().ToTable("evaluation_criteria");

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