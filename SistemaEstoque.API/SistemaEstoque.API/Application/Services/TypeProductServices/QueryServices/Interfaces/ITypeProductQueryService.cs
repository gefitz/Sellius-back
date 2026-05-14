using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.TypeProductServices.QueryServices.Interfaces;

public interface ITypeProductQueryService
{
    Task<TypeProductEdit> FindByTypeProductId(long typeProductId);
    Task<List<TypeProductTableReturn>> FindAllTypeProducts(TypeProductFilter filter, Guid enterpriseId);
}
