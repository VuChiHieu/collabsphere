using Microsoft.EntityFrameworkCore;
using FileService.Models.Entities;
using System.Linq.Expressions;

namespace FileService.Data;

public class FileServiceDbContext : DbContext
{
    public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Resource> Resources { get; set; }
    public DbSet<ResourceAccess> ResourceAccesses { get; set; }
    public DbSet<ResourceVersion> ResourceVersions { get; set; }
    public DbSet<SharedDocument> SharedDocuments { get; set; }
    public DbSet<DocumentCollaborator> DocumentCollaborators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<Resource>().ToTable("resources");
        modelBuilder.Entity<ResourceAccess>().ToTable("resource_accesses");
        modelBuilder.Entity<ResourceVersion>().ToTable("resource_versions");
        modelBuilder.Entity<SharedDocument>().ToTable("shared_documents");
        modelBuilder.Entity<DocumentCollaborator>().ToTable("document_collaborators");

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