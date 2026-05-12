using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntitysSaleOrder;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface ISaleOrderMapper
{
    SaleOrder DtoRegisterToMain(SaleOrderRegister dto, Guid userId, Guid enterpriseId);
    SaleOrderEdit MainToDtoEdit(SaleOrder order);
    List<SaleOrderTableReturn> MainToTableList(List<SaleOrder> orders);
}
