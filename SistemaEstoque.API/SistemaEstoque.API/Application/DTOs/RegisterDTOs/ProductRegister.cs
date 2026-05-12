namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class ProductRegister
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set;}
        public long TypeProduct { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        public int Active { get; set; }
    }


}
