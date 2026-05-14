using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityProduct;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface IPriceTableMapper
{
    PriceTable DtoRegisterToMain(PriceTableRegister dto, Guid enterpriseId, Guid userId);
    PriceTableEdit MainToDtoEdit(PriceTable priceTable);
    List<PriceTableTableReturn> MainToTableList(List<PriceTable> priceTables);
}
