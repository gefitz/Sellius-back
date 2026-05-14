namespace Sellius.API.Application.DTOs.TablesDTOs;

public class PriceTableXProductTableReturn
{
    public string Product { get; set; }
    public string Supplier { get; set; }
    public decimal ProductPrice { get; set; }
    public long PriceTableId { get; set; }
}
