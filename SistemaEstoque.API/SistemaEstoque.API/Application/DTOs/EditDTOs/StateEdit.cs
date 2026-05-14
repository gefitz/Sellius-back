namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record StateEdit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Acronym { get; set; }
}
