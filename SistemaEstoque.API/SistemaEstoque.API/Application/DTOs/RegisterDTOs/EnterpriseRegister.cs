using Sellius.API.Domain.Enums;

namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class EnterpriseRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public TypeLicense TypeLicense { get; set; }
        public short Active { get; set; }

        public UserRegister UserRegister { get; set; }

    }
}
