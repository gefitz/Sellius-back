using Sellius.API.Domain.Models;

namespace Sellius.API.Application.DTOs.Filters
{
    public sealed record UserFilter : PaginationFilter
    {
        public string? Name { get; set; }
        public int TypeUser { get; set; } = -1;
        public string? Document { get; set; }
        public int City { get; set; }= -1;
        public int State { get; set; } = -1;
        public short Active { get; set; } = -1;
    }   
}
