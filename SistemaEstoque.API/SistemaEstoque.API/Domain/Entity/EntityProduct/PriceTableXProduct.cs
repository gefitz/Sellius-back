namespace Sellius.API.Domain.Entity.EntityProduct
{
    public class PriceTableXProduct
    {
        public long PriceTableId { get; init; }
        public long ProductId { get; init; }
        public decimal Price { get; set; }
        public Product? Produto { get; set; }
        public PriceTable? PriceTable { get; set; }
    }
}
