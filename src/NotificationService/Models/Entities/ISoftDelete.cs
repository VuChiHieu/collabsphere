// Lưu ý: Notification thường không cần SoftDelete, nhưng tôi vẫn tạo theo yêu cầu cấu trúc chung.
namespace NotificationService.Models.Entities;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
}