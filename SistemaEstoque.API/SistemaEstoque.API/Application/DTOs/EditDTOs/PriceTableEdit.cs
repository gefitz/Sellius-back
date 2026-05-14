namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record PriceTableEdit
{
    public long Id { get; set; }
    public string DescPriceTable { get; set; }
    public DateTime InitialValidateDate { get; set; }
    public DateTime? FinalValidateDate { get; set; }
    public long SupplierId { get; set; }
    public string SupplierName { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? AlteredDate { get; set; }
    public Guid EnterpriseId { get; set; }
    public Guid UserId { get; set; }
}
