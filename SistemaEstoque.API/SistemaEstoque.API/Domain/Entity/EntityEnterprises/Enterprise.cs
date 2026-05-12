using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Entity.EntityEnterprises
{
    public class Enterprise
    {
        public Guid Id{ get; set; }
        public required string Name { get; set; }
        public required string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long CityId { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public Guid LicencaId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }
        
        public City City { get; set; }
        public License? License { get; set; }
    }
}
