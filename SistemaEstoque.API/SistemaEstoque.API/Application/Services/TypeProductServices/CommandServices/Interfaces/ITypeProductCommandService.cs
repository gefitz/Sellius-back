using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.TypeProductServices.CommandServices.Interfaces;

public interface ITypeProductCommandService
{
    Task<bool> CreateTypeProduct(TypeProductRegister dto, Guid enterpriseId);
    Task<bool> UpdateTypeProduct(TypeProductRegister dto);
    Task<bool> InactiveTypeProduct(long typeProductId);
}
