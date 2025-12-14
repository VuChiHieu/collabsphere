using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models.Entities;

[Table("notification_queues")]
public class NotificationQueue : BaseEntity
{
    [Key]
    public Guid QueueId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string NotificationType { get; set; } = "EMAIL"; // EMAIL, PUSH, IN_APP

    [Required]
    [Column(TypeName = "text")]
    public string Payload { get; set; } = string.Empty; // JSON: notification data

    [MaxLength(50)]
    public string Status { get; set; } = "PENDING"; // PENDING, PROCESSING, COMPLETED, FAILED

    public DateTime? ScheduledFor { get; set; } // For scheduled notifications

    public DateTime? ProcessedAt { get; set; }

    public int RetryCount { get; set; } = 0;

    public int MaxRetries { get; set; } = 3;

    [Column(TypeName = "text")]
    public string? ErrorMessage { get; set; }

    [MaxLength(50)]
    public string Priority { get; set; } = "NORMAL"; // LOW, NORMAL, HIGH
}
