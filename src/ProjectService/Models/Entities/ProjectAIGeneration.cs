using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Models.Entities;

[Table("project_ai_generations")]
public class ProjectAIGeneration : BaseEntity
{
    [Key]
    public Guid GenerationId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string PromptUsed { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "text")]
    public string GeneratedContent { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Model { get; set; } = "gemini-pro";

    public int? TokensUsed { get; set; }

    [Required]
    public Guid GeneratedBy { get; set; }

    // Navigation properties
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}