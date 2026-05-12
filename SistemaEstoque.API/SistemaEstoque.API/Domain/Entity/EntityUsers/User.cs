using System.ComponentModel.DataAnnotations;
using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.Domain.Entity.EntitysSaleOrder;

namespace Sellius.API.Domain.Entity.EntityUsers
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public long TpUsuarioId { get; set; }
        public short Active { get; set; }
        public Guid EnterpriseId { get; set; }
        public long CityId { get; set; }

        public Enterprise? Enterprise { get; set; }
        public TypeUser? TypeUser { get; set; }
        public List<SaleOrder>? SaleOrder { get; set; }
        public City? City { get; set; } = new City();
    }
}
