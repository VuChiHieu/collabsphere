// src/AcademicService/Models/Entities/BaseEntity.cs
namespace AcademicService.Models.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}