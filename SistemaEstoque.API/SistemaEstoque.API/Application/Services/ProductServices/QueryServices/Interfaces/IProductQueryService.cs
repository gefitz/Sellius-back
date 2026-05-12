using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Application.Services.ProductServices.QueryServices.Interfaces;

public interface IProductQueryService
{
    Task<ProductEdit> FindByProductId(long productId);
    Task<List<ProductTableReturn>> FindAllProducts(ProductFilter filter, Guid enterpriseId);
}
