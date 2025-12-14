// src/UserService/Models/Entities/ISoftDelete.cs
namespace UserService.Models.Entities;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
}