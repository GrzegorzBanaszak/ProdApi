using System;

namespace ProdApi.Models;

public sealed class Tag
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }

    public IReadOnlyList<TaskItem> Tasks => _tasks;
    private List<TaskItem> _tasks = new();

    private Tag() { }
    private Tag(string name)
    {
        Name = name;
    }

    public static Tag Create(string name)
    {
        return new Tag(name);
    }

}
