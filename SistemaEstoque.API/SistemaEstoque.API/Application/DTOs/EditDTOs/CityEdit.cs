namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record CityEdit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long StateId { get; set; }
    public string StateName { get; set; }
}
