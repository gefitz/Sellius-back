using Microsoft.EntityFrameworkCore;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.SaleOrderServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Pedidos.Interfaces;

namespace Sellius.API.Application.Services.SaleOrderServices.QueryServices;

public sealed class SaleOrderQueryService(
    IPedidoRepository repository,
    ISaleOrderMapper mapper) : ISaleOrderQueryService
{
    public async Task<SaleOrderEdit> FindBySaleOrderId(long orderId)
    {
        var order = await repository.FindByPredicateAsync(
            o => o.Id == orderId,
            q => q.Include(o => o.Customer).Include(o => o.User),
            true);

        return order is not null ? mapper.MainToDtoEdit(order) : new SaleOrderEdit();
    }

    public async Task<List<SaleOrderTableReturn>> FindAllSaleOrders(SaleOrderFilter filter, Guid enterpriseId)
    {
        var orders = await repository.FindAllAsync(
            o => o.EnterpriseId == enterpriseId
                 && (filter.CustomerName == null || o.Customer.Name.Contains(filter.CustomerName))
                 && (filter.UserName == null || o.User != null && o.User.Name.Contains(filter.UserName)),
            q => q.Include(o => o.Customer).Include(o => o.User),
            o => o.OrderByDescending(s => s.OrderCreateDate));

        return mapper.MainToTableList(orders);
    }
}
