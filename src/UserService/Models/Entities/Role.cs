using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities;

[Table("roles")]
public class Role : BaseEntity
{
    [Key]
    public Guid RoleId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    // Navigation properties
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}