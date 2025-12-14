using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileService.Models.Entities;

[Table("shared_documents")]
public class SharedDocument : BaseEntity, ISoftDelete
{
    [Key]
    public Guid DocumentId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; } // Denormalized from TeamService

    [Required]
    [MaxLength(300)]
    public string DocumentName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string DocumentType { get; set; } = "TEXT"; // TEXT, SPREADSHEET, PRESENTATION

    [Column(TypeName = "text")]
    public string Content { get; set; } = string.Empty; // Document content (for real-time editing)

    public bool IsLocked { get; set; } = false; // Lock for editing

    public Guid? LockedBy { get; set; } // User who locked the document

    public DateTime? LockedAt { get; set; }

    public int Version { get; set; } = 1;

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<DocumentCollaborator> DocumentCollaborators { get; set; } = new List<DocumentCollaborator>();
}