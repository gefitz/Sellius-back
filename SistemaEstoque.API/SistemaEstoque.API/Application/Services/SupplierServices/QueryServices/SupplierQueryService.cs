using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.SupplierServices.QueryServices.Interfaces;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Infra.Repository.Fornecedor.Interfaces;

namespace Sellius.API.Application.Services.SupplierServices.QueryServices;

public sealed class SupplierQueryService(
    ISupplierRepository repository,
    ISupplierMapper mapper) : ISupplierQueryService
{
    public async Task<SupplierEdit> FindBySupplierId(long supplierId)
    {
        var supplier = await repository.FindByPredicateAsync(
            s => s.Id == supplierId);

        return supplier is not null ? mapper.MainToDtoEdit(supplier) : new SupplierEdit();
    }

    public async Task<List<SupplierTableReturn>> FindAllSuppliers(SupplierFilter filter, Guid enterpriseId)
    {
        var suppliers = await repository.FindAllAsync(
            s => s.EnterpriseId == enterpriseId
                 && (filter.Name == null || s.Name.Contains(filter.Name))
                 && (filter.Document == null || s.Document.Contains(filter.Document))
                 && (filter.State < 0 || s.Active == filter.State),
            null,
            o => o.OrderBy(s => s.Name));

        return mapper.MainToTableList(suppliers);
    }
}
