using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntitysSaleOrder;
using Sellius.API.Domain.Enums;

namespace Sellius.API.Application.Mappers;

public sealed class SaleOrderMapper : ISaleOrderMapper
{
    public SaleOrder DtoRegisterToMain(SaleOrderRegister dto, Guid userId, Guid enterpriseId) =>
        new SaleOrder
        {
            CustomerId = dto.CustomerId,
            UserId = userId,
            EnterpriseId = enterpriseId,
            OrderCreateDate = dto.DateSale == default ? DateTime.UtcNow : dto.DateSale,
            Finished = 0
        };

    public SaleOrderEdit MainToDtoEdit(SaleOrder order) =>
        new SaleOrderEdit
        {
            Id = order.Id,
            Qtd = order.Qtd,
            CustomerId = order.CustomerId,
            Finished = order.Finished,
            OrderCreateDate = order.OrderCreateDate,
            UserId = order.UserId,
            EnterpriseId = order.EnterpriseId
        };

    public List<SaleOrderTableReturn> MainToTableList(List<SaleOrder> orders) =>
        orders.Select(o => new SaleOrderTableReturn
        {
            Id = o.Id,
            Volume = o.Qtd,
            Customer = o.Customer?.Name ?? string.Empty,
            User = o.User?.Name ?? string.Empty,
            State = (EStateOrder)o.Finished,
            SaleDate = o.OrderCreateDate
        }).ToList();
}
