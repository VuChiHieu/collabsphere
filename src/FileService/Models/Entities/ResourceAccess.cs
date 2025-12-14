using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileService.Models.Entities;

[Table("resource_accesses")]
public class ResourceAccess : BaseEntity
{
    [Key]
    public Guid AccessId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ResourceId { get; set; }

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [Required]
    [MaxLength(50)]
    public string AccessType { get; set; } = "VIEW"; // VIEW, DOWNLOAD, EDIT

    public DateTime AccessedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    public string? IpAddress { get; set; }

    [MaxLength(500)]
    public string? UserAgent { get; set; }

    // Navigation properties
    [ForeignKey("ResourceId")]
    public virtual Resource Resource { get; set; } = null!;
}