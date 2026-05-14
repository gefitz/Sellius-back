using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.TypeProductServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Product.Interface;

namespace Sellius.API.Application.Services.TypeProductServices.QueryServices;

public sealed class TypeProductQueryService(
    ITypeProductRepository repository,
    ITypeProductMapper mapper) : ITypeProductQueryService
{
    public async Task<TypeProductEdit> FindByTypeProductId(long typeProductId)
    {
        var typeProduct = await repository.FindByPredicateAsync(t => t.Id == typeProductId);
        return typeProduct is not null ? mapper.MainToDtoEdit(typeProduct) : new TypeProductEdit();
    }

    public async Task<List<TypeProductTableReturn>> FindAllTypeProducts(TypeProductFilter filter, Guid enterpriseId)
    {
        var typeProducts = await repository.FindAllAsync(
            t => t.EnterpriseId == enterpriseId
                 && (filter.Name == null || t.Name.Contains(filter.Name))
                 && (filter.Active < 0 || t.Active == filter.Active),
            null,
            o => o.OrderBy(t => t.Name));

        return mapper.MainToTableList(typeProducts);
    }
}
