using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.TypeProductServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.Product.Interface;

namespace Sellius.API.Application.Services.TypeProductServices.CommandServices;

public sealed class TypeProductCommandService(
    ITypeProductRepository repository,
    ITypeProductMapper mapper) : ITypeProductCommandService
{
    public async Task<bool> CreateTypeProduct(TypeProductRegister dto, Guid enterpriseId)
    {
        var typeProduct = mapper.DtoRegisterToMain(dto, enterpriseId);
        typeProduct.Active = 1;

        return await repository.CreateTypeProductAsync(typeProduct);
    }

    public async Task<bool> UpdateTypeProduct(TypeProductRegister dto)
    {
        var typeProduct = await repository.FindByPredicateAsync(t => t.Id == dto.Id);

        if (typeProduct is null)
            return false;

        typeProduct.Name = dto.Name;
        typeProduct.Description = dto.Description;

        return await repository.UpdateTypeProductAsync(typeProduct);
    }

    public async Task<bool> InactiveTypeProduct(long typeProductId)
    {
        var typeProduct = await repository.FindByPredicateAsync(t => t.Id == typeProductId);

        if (typeProduct is null)
            return false;

        typeProduct.Active = 0;

        return await repository.UpdateTypeProductAsync(typeProduct);
    }
}
