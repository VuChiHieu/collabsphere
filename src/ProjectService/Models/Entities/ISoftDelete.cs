// src/ProjectService/Models/Entities/ISoftDelete.cs
namespace ProjectService.Models.Entities;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
}