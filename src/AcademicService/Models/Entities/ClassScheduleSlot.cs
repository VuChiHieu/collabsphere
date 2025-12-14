using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicService.Models.Entities;

[Table("class_schedule_slots")]
public class ClassScheduleSlot : BaseEntity
{
    [Key]
    public Guid ScheduleSlotId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClassId { get; set; }

    [Required]
    public int SlotNumber { get; set; }

    [Required]
    public DateOnly ScheduledDate { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    [MaxLength(50)]
    public string? Room { get; set; }

    [MaxLength(500)]
    public string? Topic { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "SCHEDULED"; // SCHEDULED, COMPLETED, CANCELLED

    public DateTime? CompletedAt { get; set; }

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    // Navigation properties
    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; } = null!;
}