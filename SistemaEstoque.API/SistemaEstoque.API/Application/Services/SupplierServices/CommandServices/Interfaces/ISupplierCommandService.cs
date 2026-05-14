using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.SupplierServices.CommandServices.Interfaces;

public interface ISupplierCommandService
{
    Task<bool> CreateSupplier(SupplierRegister dto, Guid enterpriseId);
    Task<bool> UpdateSupplier(SupplierRegister dto);
    Task<bool> InactiveSupplier(long supplierId);
}
