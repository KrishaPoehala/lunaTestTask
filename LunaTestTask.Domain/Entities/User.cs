namespace LunaTestTask.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; } = [];

    public User()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }
}