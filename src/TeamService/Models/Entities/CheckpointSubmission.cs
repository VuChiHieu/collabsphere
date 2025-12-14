using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("checkpoint_submissions")]
public class CheckpointSubmission : BaseEntity
{
    [Key]
    public Guid SubmissionId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CheckpointId { get; set; }

    [Column(TypeName = "text")]
    public string? SubmissionFiles { get; set; } // JSON array of file URLs

    [Column(TypeName = "text")]
    public string? SubmissionNotes { get; set; }

    public int Version { get; set; } = 1; // Submission version (allow resubmissions)

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public Guid SubmittedBy { get; set; }

    public bool IsLatest { get; set; } = true; // Mark latest submission

    // Navigation properties
    [ForeignKey("CheckpointId")]
    public virtual Checkpoint Checkpoint { get; set; } = null!;
}