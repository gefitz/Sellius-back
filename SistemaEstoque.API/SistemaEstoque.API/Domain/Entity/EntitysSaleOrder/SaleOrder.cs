using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Entity.EntitysSaleOrder
{
    public class SaleOrder
    {
        public long Id { get; set; }
        public int Qtd { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public short Finished { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public Guid UserId { get; set; }
        public Guid EnterpriseId { get; set; }

        public List<SaleOrdeXProduct> Product { get; set; }
        public User? User { get; set; }
        public Enterprise? Enterprise { get; set; }
    }
}
