namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record MenuEdit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? UrlMenu { get; set; }
    public string Icon { get; set; }
    public long? MenuFatherId { get; set; }
    public Guid? EnterpriseId { get; set; }
    public short Active { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? AlteredDate { get; set; }
}
