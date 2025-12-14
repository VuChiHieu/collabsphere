using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileService.Models.Entities;

[Table("resources")]
public class Resource : BaseEntity, ISoftDelete
{
    [Key]
    public Guid ResourceId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(300)]
    public string ResourceName { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string FileUrl { get; set; } = string.Empty;

    public long FileSize { get; set; } = 0; // Size in bytes

    [MaxLength(100)]
    public string? FileType { get; set; } // MIME type: application/pdf, image/png

    [MaxLength(50)]
    public string Scope { get; set; } = "CLASS"; // CLASS, TEAM, MILESTONE, CHECKPOINT, USER

    [Required]
    public Guid ScopeId { get; set; } // ID of the Class/Team/Milestone/etc

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }

    [Column(TypeName = "text")]
    public string? Metadata { get; set; } // JSON: additional file metadata

    public int DownloadCount { get; set; } = 0;

    public int ViewCount { get; set; } = 0;

    public bool IsPublic { get; set; } = false;

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    [Required]
    public Guid UploadedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<ResourceAccess> ResourceAccesses { get; set; } = new List<ResourceAccess>();
    public virtual ICollection<ResourceVersion> ResourceVersions { get; set; } = new List<ResourceVersion>();
}