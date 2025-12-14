using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models.Entities;

[Table("notifications")]
public class Notification : BaseEntity, ISoftDelete
{
    [Key]
    public Guid NotificationId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [Required]
    [MaxLength(50)]
    public string NotificationType { get; set; } = "INFO"; // INFO, SUCCESS, WARNING, ERROR, MENTION, ASSIGNMENT

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "text")]
    public string Message { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Priority { get; set; } = "NORMAL"; // LOW, NORMAL, HIGH, URGENT

    [MaxLength(500)]
    public string? ActionUrl { get; set; } // Deep link to relevant page

    [Column(TypeName = "text")]
    public string? Metadata { get; set; } // JSON: additional data (entityId, entityType, etc.)

    public bool IsRead { get; set; } = false;

    public DateTime? ReadAt { get; set; }

    public bool IsDismissed { get; set; } = false;

    public DateTime? DismissedAt { get; set; }

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    public Guid? TriggeredBy { get; set; } // User who triggered the notification
}