namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record ProductEdit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public long? TypeProductId { get; set; }
    public short Active { get; set; }
    public long SupplierId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? AlteredDate { get; set; }
    public Guid EnterpriseId { get; set; }
}
