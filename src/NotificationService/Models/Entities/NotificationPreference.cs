using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models.Entities;

[Table("notification_preferences")]
public class NotificationPreference : BaseEntity
{
    [Key]
    public Guid PreferenceId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService (UNIQUE)

    // Global toggles
    public bool EmailEnabled { get; set; } = true;
    public bool InAppEnabled { get; set; } = true;
    public bool PushEnabled { get; set; } = false;

    // Specific notification types (JSON)
    [Column(TypeName = "text")]
    public string? NotificationTypes { get; set; } // JSON: {MENTION: {email: true, inApp: true}, ASSIGNMENT: {...}}

    // Quiet hours
    public TimeOnly? QuietHoursStart { get; set; }
    public TimeOnly? QuietHoursEnd { get; set; }

    public bool EnableQuietHours { get; set; } = false;

    // Digest settings
    public bool EnableDailyDigest { get; set; } = false;
    public bool EnableWeeklyDigest { get; set; } = false;

    [MaxLength(10)]
    public string? DigestTime { get; set; } // e.g., "08:00"

    [MaxLength(20)]
    public string? TimeZone { get; set; } // e.g., "Asia/Ho_Chi_Minh"
}