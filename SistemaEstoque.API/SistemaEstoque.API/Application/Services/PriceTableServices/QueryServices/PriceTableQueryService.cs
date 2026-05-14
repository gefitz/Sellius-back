using Microsoft.EntityFrameworkCore;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.PriceTableServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Product.Interface;

namespace Sellius.API.Application.Services.PriceTableServices.QueryServices;

public sealed class PriceTableQueryService(
    IPriceTableRepository repository,
    IPriceTableMapper mapper) : IPriceTableQueryService
{
    public async Task<PriceTableEdit> FindByPriceTableId(long priceTableId)
    {
        var priceTable = await repository.FindByPredicateAsync(
            p => p.Id == priceTableId,
            q => q.Include(p => p.Supplier));

        return priceTable is not null ? mapper.MainToDtoEdit(priceTable) : new PriceTableEdit();
    }

    public async Task<List<PriceTableTableReturn>> FindAllPriceTables(PriceTableFilter filter, Guid enterpriseId)
    {
        var priceTables = await repository.FindAllAsync(
            p => p.EnterpriseId == enterpriseId
                 && (filter.NamePriceTable == null || p.DescPriceTable.Contains(filter.NamePriceTable))
                 && (filter.InitialDate == default || p.InitialValidateDate >= filter.InitialDate)
                 && (filter.FinalDate == default || p.InitialValidateDate <= filter.FinalDate),
            q => q.Include(p => p.Supplier),
            o => o.OrderBy(p => p.DescPriceTable));

        return mapper.MainToTableList(priceTables);
    }
}
