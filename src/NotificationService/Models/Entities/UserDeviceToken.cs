using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models.Entities;

[Table("user_device_tokens")]
public class UserDeviceToken : BaseEntity
{
    [Key]
    public Guid TokenId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [Required]
    [MaxLength(500)]
    public string DeviceToken { get; set; } = string.Empty; // FCM/APNs token

    [Required]
    [MaxLength(50)]
    public string DeviceType { get; set; } = "WEB"; // WEB, IOS, ANDROID

    [MaxLength(100)]
    public string? DeviceName { get; set; } // e.g., "iPhone 14", "Chrome on Windows"

    public bool IsActive { get; set; } = true;

    public DateTime? LastUsedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }
}