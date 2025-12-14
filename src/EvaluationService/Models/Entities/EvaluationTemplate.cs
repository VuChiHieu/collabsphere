using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("evaluation_templates")]
public class EvaluationTemplate : BaseEntity
{
    [Key]
    public Guid TemplateId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public string TemplateName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string EvaluationType { get; set; } = "GENERAL"; // TEAM, MEMBER, PEER, CHECKPOINT, MILESTONE

    [Column(TypeName = "text")]
    public string? Criteria { get; set; } // JSON array of criteria with weights

    public bool IsPublic { get; set; } = false; // Available to all lecturers

    public bool IsActive { get; set; } = true;

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}