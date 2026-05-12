namespace Sellius.API.Domain.Models;

public record PaginationFilter
{
    public int PageSize { get; init; } = 15;
    public int CurrentPage { get; init; }
}