using System.ComponentModel.DataAnnotations;
using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.Domain.Entity.EntitysSaleOrder;

namespace Sellius.API.Domain.Entity.EntityCustomers
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public long CityId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public short Active { get; set; }
        public long? SegmentationId { get; set; }
        public Guid EnterpriseId { get; set; }
        public long? GroupId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        
        public City City { get; set; }
        public Segmentation? Segmentation { get; set; }
        public Enterprise? Enterprise { get; set; }
        public GroupCustomer? Gruop { get; set; }
        public List<SupplierXCustomer>? SupplierXCustomer { get; set; }
        public List<SaleOrder>? SaleOrders { get; set; }
    }
}
