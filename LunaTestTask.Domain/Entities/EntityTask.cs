namespace LunaTestTask.Domain.Entities;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate{ get; set; }

    public TaskEntityStatus Status { get; set; }

    public TaskEnitityPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public TaskEntity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        Priority = TaskEnitityPriority.Medium;
        Status = TaskEntityStatus.InProgress;
    }
}