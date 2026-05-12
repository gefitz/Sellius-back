using Sellius.API.Domain.Models;

namespace Sellius.API.Application.DTOs.Filters
{
    public sealed record MenuFilter: PaginationFilter
    {
        public string? Menu { get; set; }
        public short Active { get; set; } = -1;
    }
}
