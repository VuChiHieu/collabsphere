using Microsoft.EntityFrameworkCore;
using CommunicationService.Models.Entities;
using System.Linq.Expressions;

namespace CommunicationService.Data;

public class CommunicationServiceDbContext : DbContext
{
    public CommunicationServiceDbContext(DbContextOptions<CommunicationServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelMember> ChannelMembers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageReaction> MessageReactions { get; set; }
    public DbSet<MessageRead> MessageReads { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
    public DbSet<MeetingNote> MeetingNotes { get; set; }
    public DbSet<DirectMessage> DirectMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommunicationServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<Channel>().ToTable("channels");
        modelBuilder.Entity<ChannelMember>().ToTable("channel_members");
        modelBuilder.Entity<Message>().ToTable("messages");
        modelBuilder.Entity<MessageReaction>().ToTable("message_reactions");
        modelBuilder.Entity<MessageRead>().ToTable("message_reads");
        modelBuilder.Entity<Meeting>().ToTable("meetings");
        modelBuilder.Entity<MeetingParticipant>().ToTable("meeting_participants");
        modelBuilder.Entity<MeetingNote>().ToTable("meeting_notes");
        modelBuilder.Entity<DirectMessage>().ToTable("direct_messages");

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