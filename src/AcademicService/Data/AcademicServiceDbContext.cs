using Microsoft.EntityFrameworkCore;
using AcademicService.Models.Entities;
using System.Linq.Expressions;

namespace AcademicService.Data;

public class AcademicServiceDbContext : DbContext
{
    public AcademicServiceDbContext(DbContextOptions<AcademicServiceDbContext> options) 
        : base(options)
    {
    }

    // DbSets
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Syllabus> Syllabi { get; set; }
    public DbSet<SyllabusWeek> SyllabusWeeks { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassLecturer> ClassLecturers { get; set; }
    public DbSet<ClassStudent> ClassStudents { get; set; }
    public DbSet<ClassScheduleSlot> ClassScheduleSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("academicservice");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademicServiceDbContext).Assembly);

        // Table naming convention
        modelBuilder.Entity<Subject>().ToTable("subjects");
        modelBuilder.Entity<Syllabus>().ToTable("syllabi");
        modelBuilder.Entity<SyllabusWeek>().ToTable("syllabus_weeks");
        modelBuilder.Entity<Class>().ToTable("classes");
        modelBuilder.Entity<ClassLecturer>().ToTable("class_lecturers");
        modelBuilder.Entity<ClassStudent>().ToTable("class_students");
        modelBuilder.Entity<ClassScheduleSlot>().ToTable("class_schedule_slots");

        // Apply soft delete filter
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                var condition = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);
                
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
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
}