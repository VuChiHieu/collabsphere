using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Models.Entities;

[Table("project_objectives")]
public class ProjectObjective : BaseEntity
{
    [Key]
    public Guid ObjectiveId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ObjectiveCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "text")]
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? BloomLevel { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal Weight { get; set; } = 0;

    [Required]
    public int Order { get; set; }

    // Navigation properties
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}