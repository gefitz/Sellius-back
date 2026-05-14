namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record SegmentationEdit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public short Active { get; set; }
    public Guid EnterpriseId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime AlteredDate { get; set; }
}
