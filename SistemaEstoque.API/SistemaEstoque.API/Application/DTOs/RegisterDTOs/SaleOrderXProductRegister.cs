namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class SaleOrderXProductRegister
    {
        public long SaleOrderId { get; set; }
        public int Qtde { get; set; }
        public decimal Price { get; set; }
        public long ProductId { get; set; }
    }
}
