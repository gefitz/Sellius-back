using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.SupplierServices.CommandServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.Infra.Repository.Fornecedor.Interfaces;

namespace Sellius.API.Application.Services.SupplierServices.CommandServices;

public sealed class SupplierCommandService(
    ISupplierRepository repository,
    ISupplierMapper mapper) : ISupplierCommandService
{
    public async Task<bool> CreateSupplier(SupplierRegister dto, Guid enterpriseId)
    {
        if (await SupplierAlreadyExists(dto.Document, enterpriseId))
            return false;

        var supplier = mapper.DtoRegisterToMain(dto, enterpriseId);
        supplier.Document = supplier.Document.Hash();
        supplier.CreateDate = DateTime.UtcNow;
        supplier.AlteredDate = DateTime.UtcNow;
        supplier.Active = 1;

        return await repository.CreateSupplierAsync(supplier);
    }

    public async Task<bool> UpdateSupplier(SupplierRegister dto)
    {
        var supplier = await repository.FindByPredicateAsync(
            s => s.Id == dto.Id);

        if (supplier is null)
            return false;

        supplier.Name = dto.Name;
        supplier.Document = dto.Document.Hash();
        supplier.Phone = dto.Phone;
        supplier.Email = dto.Email;
        supplier.CityId = dto.CityId;
        supplier.ZipCode = dto.ZipCode;
        supplier.Street = dto.Street;
        supplier.Neighborhood = dto.Neighborhood;
        supplier.Complement = dto.Complement;
        supplier.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateSupplierAsync(supplier);
    }

    public async Task<bool> InactiveSupplier(long supplierId)
    {
        var supplier = await repository.FindByPredicateAsync(
            s => s.Id == supplierId);

        if (supplier is null)
            return false;

        supplier.Active = 0;
        supplier.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateSupplierAsync(supplier);
    }

    private async Task<bool> SupplierAlreadyExists(string document, Guid enterpriseId)
    {
        var existing = await repository.FindByPredicateAsync(
            s => s.Document == document && s.EnterpriseId == enterpriseId);

        return existing is not null;
    }
}
