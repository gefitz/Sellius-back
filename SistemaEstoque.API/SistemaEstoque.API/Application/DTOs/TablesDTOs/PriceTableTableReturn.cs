namespace Sellius.API.Application.DTOs.TablesDTOs;

public class PriceTableTableReturn
{
    public long Id { get; set; }
    public string DescPriceTable { get; set; }
    public DateTime InitialValidateDate { get; set; }
    public DateTime? FinalValidateDate { get; set; }
    public long SupplierId { get; set; }
    public string Supplier { get; set; }
    public DateTime CreateDate { get; set; }
}
