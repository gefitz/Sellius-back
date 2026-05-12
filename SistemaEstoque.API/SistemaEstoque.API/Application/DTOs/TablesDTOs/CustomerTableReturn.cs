namespace Sellius.API.Application.DTOs.TablesDTOs
{
    public class CustomerTableReturn
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }
        public string? Group { get; set; }
        public string? Segmentation { get; set; }
    }
}
