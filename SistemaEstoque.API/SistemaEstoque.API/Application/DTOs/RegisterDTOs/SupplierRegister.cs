using Sellius.API.Domain.Entity;

namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class SupplierRegister
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public short Active { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime AlteredDate { get; set; }
        public long CityId { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string? Complement { get; set; }
    }
}
