using Sellius.API.Domain.Enums;

namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class SaleOrderRegister
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public EStateOrder State { get; set; }
        public DateTime DateSale { get; set; } 
    }
}
