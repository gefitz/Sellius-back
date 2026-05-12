namespace Sellius.API.Application.DTOs.Filters
{
    public class CustomerFilter
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
        public int CityId { get; set; }
        public short Active { get; set; }
    }
}
