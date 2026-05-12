using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityEnterprises;

namespace Sellius.API.Application.Mappers;

public sealed class EnterpriseMapper : IEnterpriseMapper
{
    public Enterprise DtoRegisterToMain(EnterpriseRegister dto) =>
        new Enterprise
        {
            Name = dto.Name,
            Document = dto.Document,
            Phone = dto.Phone,
            Email = dto.Email,
            CityId = dto.CityId,
            ZipCode = dto.ZipCode,
            Street = dto.Street,
            CreateDate = dto.CreateDate == default ? DateTime.UtcNow : dto.CreateDate,
            AlteredDate = dto.AlteredDate == default ? DateTime.UtcNow : dto.AlteredDate,
            Active = dto.Active
        };

    public EnterpriseEdit MainToDtoEdit(Enterprise enterprise) =>
        new EnterpriseEdit
        {
            Id = enterprise.Id,
            Name = enterprise.Name,
            Document = enterprise.Document,
            Phone = enterprise.Phone,
            Email = enterprise.Email,
            CityId = enterprise.CityId,
            ZipCode = enterprise.ZipCode,
            Street = enterprise.Street,
            LicencaId = enterprise.LicencaId,
            CreateDate = enterprise.CreateDate,
            AlteredDate = enterprise.AlteredDate,
            Active = enterprise.Active
        };
}
