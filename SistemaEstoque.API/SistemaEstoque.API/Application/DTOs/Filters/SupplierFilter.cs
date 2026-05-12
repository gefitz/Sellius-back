using Sellius.API.Domain.Models;

namespace Sellius.API.DTOs.Filtros
{
    public sealed record SupplierFilter :  PaginationFilter
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
        public int City { get; set; }
        public int StateId { get; set; }
        public short State { get; set; }
    }
}
