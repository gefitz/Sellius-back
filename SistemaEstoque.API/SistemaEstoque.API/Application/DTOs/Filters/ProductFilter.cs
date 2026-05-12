using Sellius.API.Domain.Models;

namespace Sellius.API.DTOs.Filtros
{
    public sealed record ProductFilter : PaginationFilter
    {

        public string? Name { get; set; }
        public long TypeProductId { get; set; }
        public short Active { get; set; }
        public string? Supplier { get; set; }
    }
}
