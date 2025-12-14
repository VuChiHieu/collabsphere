using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileService.Models.Entities;

[Table("document_collaborators")]
public class DocumentCollaborator : BaseEntity
{
    [Key]
    public Guid CollaboratorId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid DocumentId { get; set; }

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [MaxLength(50)]
    public string Permission { get; set; } = "VIEW"; // VIEW, EDIT, ADMIN

    public DateTime? LastAccessedAt { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("DocumentId")]
    public virtual SharedDocument SharedDocument { get; set; } = null!;
}