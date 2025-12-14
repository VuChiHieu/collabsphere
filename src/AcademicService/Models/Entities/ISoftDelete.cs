// src/AcademicService/Models/Entities/ISoftDelete.cs
namespace AcademicService.Models.Entities;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
}