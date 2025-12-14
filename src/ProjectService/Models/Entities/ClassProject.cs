using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Models.Entities;

[Table("class_projects")]
public class ClassProject : BaseEntity
{
    [Key]
    public Guid ClassProjectId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClassId { get; set; } // Denormalized from AcademicService

    [Required]
    public Guid ProjectId { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public Guid AssignedBy { get; set; }

    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}