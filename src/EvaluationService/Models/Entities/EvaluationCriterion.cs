using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("evaluation_criteria")]
public class EvaluationCriterion : BaseEntity
{
    [Key]
    public Guid CriterionId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string EvaluationType { get; set; } = "GENERAL"; // TEAM, MEMBER, PEER, etc.

    [Required]
    [MaxLength(200)]
    public string CriterionName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal MaxScore { get; set; } = 10; // Maximum score for this criterion

    [Column(TypeName = "decimal(5,2)")]
    public decimal Weight { get; set; } = 1; // Weight in overall score calculation

    public int Order { get; set; } = 0; // Display order

    public bool IsActive { get; set; } = true;

    public Guid? CreatedBy { get; set; }
}