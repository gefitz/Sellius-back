using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.ProductServices.QueryServices.Interfaces;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Infra.Repository.Product.Interface;

namespace Sellius.API.Application.Services.ProductServices.QueryServices;

public sealed class ProductQueryService(
    IProductRepository repository,
    IProductMapper mapper) : IProductQueryService
{
    public async Task<ProductEdit> FindByProductId(long productId)
    {
        var product = await repository.FindByPredicateAsync(
            p => p.Id == productId, null, true);

        return product is not null ? mapper.MainToDtoEdit(product) : new ProductEdit();
    }

    public async Task<List<ProductTableReturn>> FindAllProducts(ProductFilter filter, Guid enterpriseId)
    {
        var products = await repository.FindAllAsync(
            p => p.EnterpriseId == enterpriseId
                 && (filter.Name == null || p.Name.Contains(filter.Name))
                 && (filter.TypeProductId <= 0 || p.TypeProductId == filter.TypeProductId)
                 && (filter.Active < 0 || p.Active == filter.Active),
            null,
            o => o.OrderBy(p => p.Name));

        return mapper.MainToTableList(products);
    }
}
