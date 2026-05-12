using Sellius.API.Domain.Entity.EntityEnterprises;

namespace Sellius.API.Domain.Entity.EntityProduct
{
    public class PriceTable
    {
        public long Id { get; set; }
        public required string DescPriceTable { get; set; }
        public DateTime InitialValidateDate { get; set; }
        public DateTime? FinalValidateDate { get; set; }
        public long SupplierId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        public Guid EnterpriseId { get; set; }
        public Guid UserId { get; set; }

        public Enterprise? Enterprise { get; set; }
        public Supplier? Supplier { get; set; }
        public List<PriceTableXProduct>? PriceTableXProducts { get; set; }
    }
}
