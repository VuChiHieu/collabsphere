using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities;

[Table("user_profiles")]
public class UserProfile : BaseEntity
{
    [Key]
    public Guid ProfileId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [MaxLength(50)]
    public string? StudentCode { get; set; }

    [MaxLength(50)]
    public string? LecturerCode { get; set; }

    [MaxLength(50)]
    public string? StaffCode { get; set; }

    public Guid? DepartmentId { get; set; }

    [Column(TypeName = "text")]
    public string? Bio { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [MaxLength(10)]
    public string? Gender { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("DepartmentId")]
    public virtual Department? Department { get; set; }
}