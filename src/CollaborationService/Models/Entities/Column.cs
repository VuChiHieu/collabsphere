using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("columns")]
public class Column : BaseEntity
{
    [Key]
    public Guid ColumnId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid BoardId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ColumnName { get; set; } = string.Empty; // e.g., "To Do", "In Progress", "Done"

    [MaxLength(50)]
    public string ColumnType { get; set; } = "CUSTOM"; // TODO, IN_PROGRESS, DONE, CUSTOM

    [MaxLength(20)]
    public string? Color { get; set; } // Hex color code

    public int WipLimit { get; set; } = 0; // Work In Progress limit (0 = no limit)

    [Required]
    public int Order { get; set; } // Display order

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("BoardId")]
    public virtual Board Board { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}