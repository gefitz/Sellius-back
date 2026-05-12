namespace Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs
{
    public class CustomerRegister
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }

        public long CityId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string? Complement { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreateDate { get; set; } =  DateTime.UtcNow;
        public DateTime AlteredDate { get; set; } = DateTime.UtcNow;
        public string Email { get; set; }
        public string Phone { get; set; }
        public short Active { get; set; }
        public long? SegmentionId { get; set; }
        public long? GroupId { get; set; }
    }
}
