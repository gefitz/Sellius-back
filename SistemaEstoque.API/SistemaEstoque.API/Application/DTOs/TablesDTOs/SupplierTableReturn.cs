namespace Sellius.API.Application.DTOs.TablesDTOs
{
    public class SupplierTableReturn
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public short Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public string City { get; set; }
    }
}
