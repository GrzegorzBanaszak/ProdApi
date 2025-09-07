using ProdApi.Models;

namespace ProdApi.Dtos;

public sealed record UpdateTaskItemDto(
    string Title,
    string? Description,
    TaskItemStatus? Status
);
