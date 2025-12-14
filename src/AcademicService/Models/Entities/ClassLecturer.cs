using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicService.Models.Entities;

[Table("class_lecturers")]
public class ClassLecturer : BaseEntity
{
    [Key]
    public Guid ClassLecturerId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClassId { get; set; }

    [Required]
    public Guid LecturerId { get; set; } // Denormalized from UserService

    [MaxLength(50)]
    public string Role { get; set; } = "PRIMARY"; // PRIMARY, ASSISTANT, GUEST

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public Guid? AssignedBy { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; } = null!;
}