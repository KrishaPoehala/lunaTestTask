﻿using LunaTestTask.Domain.Entities;

namespace LunaTestTask.Application.Common.Dtos;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskEntityStatus Status { get; set; }
    public TaskEnitityPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public UserDto User { get; set; }
}