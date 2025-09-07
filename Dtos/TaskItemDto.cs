using ProdApi.Models;

namespace ProdApi.Dtos;

public class TaskItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public List<string> Tags { get; set; } = new();
}