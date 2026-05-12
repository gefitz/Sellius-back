namespace Sellius.API.Application.DTOs.EditDTOs;

public sealed record SupplierEdit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string ZipCode { get; set; }
    public string Street { get; set; }
    public string Neighborhood { get; set; }
    public string? Complement { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public short Active { get; set; }
    public long CityId { get; set; }
    public Guid EnterpriseId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime AlteredDate { get; set; }
}
