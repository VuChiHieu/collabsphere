using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities;

[Table("refresh_tokens")]
public class RefreshToken : BaseEntity
{
    [Key]
    public Guid TokenId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Token { get; set; } = string.Empty;

    [Required]
    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public bool IsRevoked { get; set; } = false;

    [MaxLength(500)]
    public string? DeviceInfo { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}