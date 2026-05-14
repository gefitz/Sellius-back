using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.PriceTableServices.QueryServices.Interfaces;

public interface IPriceTableQueryService
{
    Task<PriceTableEdit> FindByPriceTableId(long priceTableId);
    Task<List<PriceTableTableReturn>> FindAllPriceTables(PriceTableFilter filter, Guid enterpriseId);
}
