using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.SaleOrderServices.QueryServices.Interfaces;

public interface ISaleOrderQueryService
{
    Task<SaleOrderEdit> FindBySaleOrderId(long orderId);
    Task<List<SaleOrderTableReturn>> FindAllSaleOrders(SaleOrderFilter filter, Guid enterpriseId);
}
