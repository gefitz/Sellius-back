namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record UserEdit
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }

    public DateTime CreateDate { get; set; }
    public DateTime AlteredDate { get; set; }
    public long TypeUserId { get; set; }
    public short Active { get; set; }
    public Guid EnterpriseId { get; set; }
    public long CityId { get; set; }
}