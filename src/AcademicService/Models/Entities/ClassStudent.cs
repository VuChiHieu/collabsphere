using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicService.Models.Entities;

[Table("class_students")]
public class ClassStudent : BaseEntity
{
    [Key]
    public Guid ClassStudentId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClassId { get; set; }

    [Required]
    public Guid StudentId { get; set; } // Denormalized from UserService

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    public string Status { get; set; } = "ENROLLED"; // ENROLLED, DROPPED, COMPLETED

    [Column(TypeName = "decimal(5,2)")]
    public decimal? FinalGrade { get; set; }

    public Guid? EnrolledBy { get; set; }
    public DateTime? DroppedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; } = null!;
}