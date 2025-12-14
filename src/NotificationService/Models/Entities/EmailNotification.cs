using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models.Entities;

[Table("email_notifications")]
public class EmailNotification : BaseEntity
{
    [Key]
    public Guid EmailId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [Required]
    [MaxLength(50)]
    public string EmailType { get; set; } = "GENERAL"; // GENERAL, INVITATION, REMINDER, ALERT

    [Required]
    [MaxLength(255)]
    public string RecipientEmail { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "text")]
    public string Body { get; set; } = string.Empty; // HTML or plain text

    [MaxLength(50)]
    public string Status { get; set; } = "PENDING"; // PENDING, SENT, FAILED, BOUNCED

    public DateTime? SentAt { get; set; }

    public int RetryCount { get; set; } = 0;

    [Column(TypeName = "text")]
    public string? ErrorMessage { get; set; }

    [Column(TypeName = "text")]
    public string? Metadata { get; set; } // JSON: additional data

    public Guid? TriggeredBy { get; set; }
}