using Sellius.API.Domain.Models;

namespace Sellius.API.DTOs.Filtros
{
    public sealed record SupplierXCustomerFilter : PaginationFilter
    {
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public string? CustomerName { get; set; }
        public string? SupplierName { get; set; }
    }
}
