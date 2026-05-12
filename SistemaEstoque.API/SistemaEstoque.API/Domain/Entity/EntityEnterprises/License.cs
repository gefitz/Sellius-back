using Sellius.API.Domain.Enums;

namespace Sellius.API.Domain.Entity.EntityEnterprises
{
    public class License
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartLicenseDate { get; set; }
        public decimal MonthlyPrice { get; set; }
        public int QtdeUserFree { get; set; }
        public decimal PriceForUser { get; set; }
        public TypeLicense TypeLicense { get; set; }
    }
}