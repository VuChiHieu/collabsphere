using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities;

[Table("permissions")]
public class Permission : BaseEntity
{
    [Key]
    public Guid PermissionId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string PermissionName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Module { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}