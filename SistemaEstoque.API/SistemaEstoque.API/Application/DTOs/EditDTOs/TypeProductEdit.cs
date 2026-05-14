namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record TypeProductEdit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public short Active { get; set; }
    public Guid EnterpriseId { get; set; }
}
