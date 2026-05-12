using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Domain.Entity
{
    public class SupplierXCustomer
    {
        public long CustomerId { get; init; }
        public long SupplierId { get; init; }
        
        public Customer? Customer { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
