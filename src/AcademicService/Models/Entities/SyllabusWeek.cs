using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicService.Models.Entities;

[Table("syllabus_weeks")]
public class SyllabusWeek : BaseEntity
{
    [Key]
    public Guid SyllabusWeekId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SyllabusId { get; set; }

    [Required]
    public int WeekNumber { get; set; }

    [Required]
    [MaxLength(500)]
    public string Topic { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "text")]
    public string? LearningObjectives { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? Activities { get; set; } // JSON

    [Required]
    public int Duration { get; set; }

    [Column(TypeName = "text")]
    public string? Materials { get; set; } // JSON

    [Required]
    public int Order { get; set; }

    // Navigation properties
    [ForeignKey("SyllabusId")]
    public virtual Syllabus Syllabus { get; set; } = null!;
}