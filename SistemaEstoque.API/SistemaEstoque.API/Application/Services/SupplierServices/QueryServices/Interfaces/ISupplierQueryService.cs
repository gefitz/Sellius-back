using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Application.Services.SupplierServices.QueryServices.Interfaces;

public interface ISupplierQueryService
{
    Task<SupplierEdit> FindBySupplierId(int supplierId);
    Task<List<SupplierTableReturn>> FindAllSuppliers(SupplierFilter filter, Guid enterpriseId);
}
