using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("boards")]
public class Board : BaseEntity
{
    [Key]
    public Guid BoardId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid WorkspaceId { get; set; }

    [Required]
    [MaxLength(200)]
    public string BoardName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    public bool IsDefault { get; set; } = false; // Default board for the workspace

    [MaxLength(20)]
    public string? Color { get; set; } // Hex color code

    public int Order { get; set; } = 0; // Display order

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; } = null!;

    public virtual ICollection<Column> Columns { get; set; } = new List<Column>();
}