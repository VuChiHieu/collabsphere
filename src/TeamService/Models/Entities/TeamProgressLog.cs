using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("team_progress_logs")]
public class TeamProgressLog : BaseEntity
{
    [Key]
    public Guid LogId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal ProgressPercentage { get; set; } // 0.00 - 100.00

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public Guid? LoggedBy { get; set; }

    // Navigation properties
    [ForeignKey("TeamId")]
    public virtual Team Team { get; set; } = null!;
}