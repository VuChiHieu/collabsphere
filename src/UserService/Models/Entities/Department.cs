using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities;

[Table("departments")]
public class Department : BaseEntity
{
    [Key]
    public Guid DepartmentId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public string DepartmentName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string DepartmentCode { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    public Guid? HeadOfDepartmentId { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}