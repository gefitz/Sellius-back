namespace Sellius.API.Application.DTOs.TablesDTOs
{
    public class ProductTableReturn
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TypeProduct { get; set; }
        public long TpProductId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        public int Active { get; set; }
        public string Supplier { get; set; }
        public long SupplierId { get; set; }
    }
}
