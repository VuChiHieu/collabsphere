using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("card_assignments")]
public class CardAssignment : BaseEntity
{
    [Key]
    public Guid AssignmentId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CardId { get; set; }

    [Required]
    public Guid AssigneeId { get; set; } // Denormalized from UserService (TeamMember)

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public Guid? AssignedBy { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("CardId")]
    public virtual Card Card { get; set; } = null!;
}