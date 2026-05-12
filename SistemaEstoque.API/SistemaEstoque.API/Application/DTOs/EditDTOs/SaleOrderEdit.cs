namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record SaleOrderEdit
{
    public long Id { get; set; }
    public int Qtd { get; set; }
    public long CustomerId { get; set; }
    public short Finished { get; set; }
    public DateTime OrderCreateDate { get; set; }
    public Guid UserId { get; set; }
    public Guid EnterpriseId { get; set; }
}
