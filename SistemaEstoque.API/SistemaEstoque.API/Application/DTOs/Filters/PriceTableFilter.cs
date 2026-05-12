using Sellius.API.Domain.Models;

namespace Sellius.API.Application.DTOs.Filters
{
    public sealed record PriceTableFilter :  PaginationFilter
    {
        public string? NamePriceTable { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}