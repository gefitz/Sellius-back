using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.PriceTableServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.Product.Interface;

namespace Sellius.API.Application.Services.PriceTableServices.CommandServices;

public sealed class PriceTableCommandService(
    IPriceTableRepository repository,
    IPriceTableMapper mapper) : IPriceTableCommandService
{
    public async Task<bool> CreatePriceTable(PriceTableRegister dto, Guid enterpriseId, Guid userId)
    {
        var priceTable = mapper.DtoRegisterToMain(dto, enterpriseId, userId);
        priceTable.CreateDate = DateTime.UtcNow;

        return await repository.CreatePriceTableAsync(priceTable);
    }

    public async Task<bool> UpdatePriceTable(PriceTableRegister dto)
    {
        var priceTable = await repository.FindByPredicateAsync(p => p.Id == dto.Id);

        if (priceTable is null)
            return false;

        priceTable.DescPriceTable = dto.Name;
        priceTable.InitialValidateDate = dto.InitialDate;
        priceTable.FinalValidateDate = dto.FinishedDate;
        priceTable.SupplierId = dto.SupplierId;
        priceTable.AlteredDate = DateTime.UtcNow;

        return await repository.UpdatePriceTableAsync(priceTable);
    }
}
