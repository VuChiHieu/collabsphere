using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("milestone_questions")]
public class MilestoneQuestion : BaseEntity
{
    [Key]
    public Guid QuestionId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamMilestoneId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string QuestionText { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string QuestionType { get; set; } = "TEXT"; // TEXT, MULTIPLE_CHOICE, FILE_UPLOAD

    [Column(TypeName = "text")]
    public string? Options { get; set; } // JSON array for multiple choice questions

    public bool IsRequired { get; set; } = true;

    [Required]
    public int Order { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal MaxScore { get; set; } = 10; // Maximum score for this question

    // Navigation properties
    [ForeignKey("TeamMilestoneId")]
    public virtual TeamMilestone TeamMilestone { get; set; } = null!;

    public virtual ICollection<MilestoneAnswer> MilestoneAnswers { get; set; } = new List<MilestoneAnswer>();
}