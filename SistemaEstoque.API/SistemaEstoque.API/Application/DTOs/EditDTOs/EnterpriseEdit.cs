namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record EnterpriseEdit
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public long CityId { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }
    public Guid LicencaId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime AlteredDate { get; set; }
    public short Active { get; set; }
}
