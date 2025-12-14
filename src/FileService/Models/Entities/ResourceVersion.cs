using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileService.Models.Entities;

[Table("resource_versions")]
public class ResourceVersion : BaseEntity
{
    [Key]
    public Guid VersionId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ResourceId { get; set; }

    [Required]
    public int VersionNumber { get; set; } // 1, 2, 3...

    [Required]
    [MaxLength(500)]
    public string FileUrl { get; set; } = string.Empty;

    public long FileSize { get; set; } = 0;

    [Column(TypeName = "text")]
    public string? ChangeDescription { get; set; }

    [Required]
    public Guid UploadedBy { get; set; }

    public bool IsCurrent { get; set; } = false; // Mark the current version

    // Navigation properties
    [ForeignKey("ResourceId")]
    public virtual Resource Resource { get; set; } = null!;
}