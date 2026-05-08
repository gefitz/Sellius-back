namespace Sellius.API.Domain.Models.Product
{
    public class PriceTableXProduct
    {
        public long PriceTableId { get; init; }
        public long ProductId { get; init; }
        public decimal vlPreco { get; set; }
        public Product Produto { get; set; }
        public PriceTable TabelaPreco { get; set; }
    }
}
