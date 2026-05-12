using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.SaleOrderServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.Pedidos.Interfaces;

namespace Sellius.API.Application.Services.SaleOrderServices.CommandServices;

public sealed class SaleOrderCommandService(
    IPedidoRepository repository,
    ISaleOrderMapper mapper) : ISaleOrderCommandService
{
    public async Task<bool> CreateSaleOrder(SaleOrderRegister dto, Guid userId, Guid enterpriseId)
    {
        var order = mapper.DtoRegisterToMain(dto, userId, enterpriseId);
        order.OrderCreateDate = DateTime.UtcNow;
        order.Finished = 0;

        return await repository.CreateSaleOrderAsync(order);
    }

    public async Task<bool> UpdateSaleOrder(SaleOrderRegister dto)
    {
        var order = await repository.FindByPredicateAsync(
            o => o.Id == dto.Id);

        if (order is null)
            return false;

        order.CustomerId = dto.CustomerId;

        return await repository.UpdateSaleOrderAsync(order);
    }

    public async Task<bool> CancelSaleOrder(long orderId)
    {
        var order = await repository.FindByPredicateAsync(
            o => o.Id == orderId);

        if (order is null)
            return false;

        if (order.Finished == 1)
            return false;

        order.Finished = 2;

        return await repository.UpdateSaleOrderAsync(order);
    }
}
