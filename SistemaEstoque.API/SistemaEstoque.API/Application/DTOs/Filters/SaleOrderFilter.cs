using Sellius.API.Domain.Enums;
using Sellius.API.Domain.Models;

namespace Sellius.API.Application.DTOs.Filters
{
    public sealed record SaleOrderFilter : PaginationFilter
    {
        public DateTime SaleDate { get; set; }
        public string? CustomerName { get; set; }
        public string? UserName { get; set; }
        public EStateOrder State { get; set; }
    }
}
