namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class PriceTableRegister
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public long SupplierId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        
    }
}
