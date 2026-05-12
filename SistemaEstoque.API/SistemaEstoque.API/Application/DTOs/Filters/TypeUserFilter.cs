using Sellius.API.Domain.Models;

namespace Sellius.API.DTOs.Filtros
{
    public sealed record TypeUserFilter : PaginationFilter
    {
        public short Active { get; set; } = -1;
        public string? Name { get; set; }
    }
}
