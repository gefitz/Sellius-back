using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record TypeUserEdit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Guid EnterpriseId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime AlteredDate { get; set; }
    public short Active { get; set; }
    public List<long>? MenuIds { get; set; }
    public UserConfiguration? UserConfiguration { get; set; }
}
