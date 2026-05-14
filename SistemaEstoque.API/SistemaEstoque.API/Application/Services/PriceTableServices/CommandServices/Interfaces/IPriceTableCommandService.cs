using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.PriceTableServices.CommandServices.Interfaces;

public interface IPriceTableCommandService
{
    Task<bool> CreatePriceTable(PriceTableRegister dto, Guid enterpriseId, Guid userId);
    Task<bool> UpdatePriceTable(PriceTableRegister dto);
}
