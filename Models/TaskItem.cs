using System;

namespace ProdApi.Models;

public sealed class TaskItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskItemStatus Status { get; private set; } = TaskItemStatus.ToDo;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IReadOnlyList<Tag> Tags => _tags;
    private List<Tag> _tags = new();


    private TaskItem() { }
    private TaskItem(string title, string? description, TaskItemStatus? status = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Title = title;
        Description = description;
        Status = status ?? TaskItemStatus.ToDo;
        CreatedAt = DateTime.UtcNow;
    }

    private void WhenUpdated() => UpdatedAt = DateTime.UtcNow;

    public static TaskItem Create(string title, string? description = null, TaskItemStatus? status = null) => new TaskItem(title, description, status);

    public void UpdateDescription(string description)
    {
        Description = description;
        WhenUpdated();
    }

    public void UpdateStatus(TaskItemStatus status)
    {
        Status = status;
        WhenUpdated();
    }

    public void UpdateTaskItem(string title, string? description = null, TaskItemStatus? status = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Title = title;
        Description = description ?? Description;
        Status = status ?? Status;
        WhenUpdated();
    }

    public void AddTag(Tag tag)
    {
        if (_tags.Any(t => t.Id == tag.Id)) return;
        _tags.Add(tag);
        WhenUpdated();
    }
    public void RemoveTag(Tag tag)
    {
        _tags.Remove(tag);
        WhenUpdated();
    }
}



