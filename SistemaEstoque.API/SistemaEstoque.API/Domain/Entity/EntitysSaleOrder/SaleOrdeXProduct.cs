using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Entity.EntitysSaleOrder
{
    public class SaleOrdeXProduct
    { 
        public long SaleOrderId { get; set; }
        public long ProductId { get; set; }
        public int Qtd { get; set; }
        public decimal PriveSeller { get; set; }

        public Product? Product { get; set; }
        public SaleOrder? SaleOrder { get; set; }
    }
}
