using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.ProductServices.CommandServices.Interfaces;
using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.Infra.Repository.Product.Interface;

namespace Sellius.API.Application.Services.ProductServices.CommandServices;

public sealed class ProductCommandService(
    IProductRepository repository,
    IProductMapper mapper) : IProductCommandServices
{
    public async Task<bool> CreateProduct(ProductRegister dto, Guid enterpriseId)
    {
        if (await ProductAlreadyExists(dto.Name, enterpriseId))
            return false;

        var product = mapper.DtoRegisterToMain(dto);
        product.EnterpriseId = enterpriseId;
        product.CreateDate = DateTime.UtcNow;
        product.Active = 1;

        return await repository.CreateProductAsync(product);
    }

    public async Task<bool> UpdateProduct(ProductRegister dto)
    {
        var product = await repository.FindByPredicateAsync(
            p => p.Id == dto.Id, null, false);

        if (product is null)
            return false;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.TypeProductId = dto.TypeProduct;
        product.Active = (short)dto.Active;
        product.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateProductAsync(product);
    }

    public async Task<bool> InactiveProduct(long productId)
    {
        var product = await repository.FindByPredicateAsync(
            p => p.Id == productId, null, false);

        if (product is null)
            return false;

        product.Active = 0;
        product.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateProductAsync(product);
    }

    private async Task<bool> ProductAlreadyExists(string name, Guid enterpriseId)
    {
        var existing = await repository.FindByPredicateAsync(
            p => p.Name == name && p.EnterpriseId == enterpriseId, null, true);

        return existing is not null;
    }
}
