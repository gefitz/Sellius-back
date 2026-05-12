using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.Domain.Entity.EntitysSaleOrder;

namespace Sellius.API.Domain.Entity.EntityProduct
{
    public class Product
    {
        public long Id { get; init; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public long? TypeProductId {  get; set; }
        public short Active { get; set; }
        public long SupplierId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public Supplier? Supplier { get; init; }
        public Enterprise? Enterprise { get; init; }

        public List<SaleOrdeXProduct>? SaleOrders { get; set; }
        public TypeProduct? TypeProduct { get; init; }
        public List<PriceTableXProduct>? PriceTableXProducts { get; init; }
        
    }
}
