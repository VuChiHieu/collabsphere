using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities;

[Table("role_permissions")]
public class RolePermission : BaseEntity
{
    [Key]
    public Guid RolePermissionId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid RoleId { get; set; }

    [Required]
    public Guid PermissionId { get; set; }

    // Navigation properties
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("PermissionId")]
    public virtual Permission Permission { get; set; } = null!;
}