using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface ISupplierMapper
{
    Supplier DtoRegisterToMain(SupplierRegister dto, Guid enterpriseId);
    SupplierEdit MainToDtoEdit(Supplier supplier);
    List<SupplierTableReturn> MainToTableList(List<Supplier> suppliers);
}
