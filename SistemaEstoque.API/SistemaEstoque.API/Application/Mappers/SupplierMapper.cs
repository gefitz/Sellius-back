using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Application.Mappers;

public sealed class SupplierMapper : ISupplierMapper
{
    public Supplier DtoRegisterToMain(SupplierRegister dto, Guid enterpriseId) =>
        new Supplier
        {
            Name = dto.Name,
            Document = dto.Document,
            Phone = dto.Phone,
            Email = dto.Email,
            CityId = dto.CityId,
            ZipCode = dto.ZipCode,
            Street = dto.Street,
            Neighborhood = dto.Neighborhood,
            Complement = dto.Complement,
            EnterpriseId = enterpriseId,
            CreateDate = DateTime.UtcNow,
            AlteredDate = DateTime.UtcNow,
            Active = 1
        };

    public SupplierEdit MainToDtoEdit(Supplier supplier) =>
        new SupplierEdit
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Document = supplier.Document,
            Phone = supplier.Phone,
            Email = supplier.Email,
            CityId = supplier.CityId,
            ZipCode = supplier.ZipCode,
            Street = supplier.Street,
            Neighborhood = supplier.Neighborhood,
            Complement = supplier.Complement,
            Active = supplier.Active,
            EnterpriseId = supplier.EnterpriseId,
            CreateDate = supplier.CreateDate,
            AlteredDate = supplier.AlteredDate
        };

    public List<SupplierTableReturn> MainToTableList(List<Supplier> suppliers) =>
        suppliers.Select(s => new SupplierTableReturn
        {
            Id = s.Id,
            Name = s.Name,
            Document = s.Document,
            Phone = s.Phone,
            Email = s.Email,
            Active = s.Active,
            CreateDate = s.CreateDate,
            AlteredDate = s.AlteredDate,
            City = s.City?.Name ?? string.Empty
        }).ToList();
}
