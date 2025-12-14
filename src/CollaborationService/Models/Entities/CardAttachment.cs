using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("card_attachments")]
public class CardAttachment : BaseEntity, ISoftDelete
{
    [Key]
    public Guid AttachmentId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CardId { get; set; }

    [Required]
    [MaxLength(300)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string FileUrl { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? FileType { get; set; } // MIME type

    public long FileSize { get; set; } = 0; // Size in bytes

    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }

    [Required]
    public Guid UploadedBy { get; set; }

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    [ForeignKey("CardId")]
    public virtual Card Card { get; set; } = null!;
}