using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.SaleOrderServices.CommandServices.Interfaces;

public interface ISaleOrderCommandService
{
    Task<bool> CreateSaleOrder(SaleOrderRegister dto, Guid userId, Guid enterpriseId);
    Task<bool> UpdateSaleOrder(SaleOrderRegister dto);
    Task<bool> CancelSaleOrder(long orderId);
}
