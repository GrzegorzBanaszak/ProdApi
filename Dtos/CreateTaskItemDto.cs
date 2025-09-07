using ProdApi.Models;
namespace ProdApi.Dtos;

public record CreateTaskItemDto(string Title, string? Description, TaskItemStatus Status);

