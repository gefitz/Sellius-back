using Sellius.API.Domain.Enums;

namespace Sellius.API.Application.DTOs.TablesDTOs
{
    public class SaleOrderTableReturn
    {
        public long Id { get; set; }
        public int Volume { get; set; }
        public string Customer { get; set; }
        public string User { get; set; }
        public EStateOrder State { get; set; }
        public DateTime SaleDate { get; set; }
        
    }
}
