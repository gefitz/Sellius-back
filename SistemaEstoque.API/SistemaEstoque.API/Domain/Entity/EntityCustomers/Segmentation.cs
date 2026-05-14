using Sellius.API.Domain.Entity.EntityEnterprises;

namespace Sellius.API.Domain.Entity.EntityCustomers
{
    public class Segmentation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public Enterprise Enterprise { get; set; }
    }
}
