using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("cards")]
public class Card : BaseEntity, ISoftDelete
{
    [Key]
    public Guid CardId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ColumnId { get; set; }

    public Guid? SprintId { get; set; } // Optional: card can be in a sprint

    [Required]
    [MaxLength(300)]
    public string CardTitle { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string CardType { get; set; } = "TASK"; // EPIC, STORY, TASK, BUG

    [MaxLength(50)]
    public string Priority { get; set; } = "MEDIUM"; // LOW, MEDIUM, HIGH, CRITICAL

    [MaxLength(50)]
    public string Status { get; set; } = "TODO"; // TODO, IN_PROGRESS, REVIEW, DONE

    public int? StoryPoints { get; set; } // Effort estimation

    public DateOnly? DueDate { get; set; }

    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    [MaxLength(20)]
    public string? Color { get; set; } // Hex color code

    [Column(TypeName = "text")]
    public string? Tags { get; set; } // JSON array of tags

    public int Order { get; set; } = 0; // Order within column

    public bool IsArchived { get; set; } = false;

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    [ForeignKey("ColumnId")]
    public virtual Column Column { get; set; } = null!;

    [ForeignKey("SprintId")]
    public virtual Sprint? Sprint { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    public virtual ICollection<CardAssignment> CardAssignments { get; set; } = new List<CardAssignment>();
    public virtual ICollection<CardComment> CardComments { get; set; } = new List<CardComment>();
    public virtual ICollection<CardAttachment> CardAttachments { get; set; } = new List<CardAttachment>();
}