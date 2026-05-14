using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityProduct;

namespace Sellius.API.Application.Mappers;

public sealed class PriceTableMapper : IPriceTableMapper
{
    public PriceTable DtoRegisterToMain(PriceTableRegister dto, Guid enterpriseId, Guid userId) => new()
    {
        DescPriceTable = dto.Name,
        InitialValidateDate = dto.InitialDate,
        FinalValidateDate = dto.FinishedDate,
        SupplierId = dto.SupplierId,
        EnterpriseId = enterpriseId,
        UserId = userId,
        CreateDate = DateTime.UtcNow,
        AlteredDate = DateTime.UtcNow
    };

    public PriceTableEdit MainToDtoEdit(PriceTable priceTable) => new()
    {
        Id = priceTable.Id,
        DescPriceTable = priceTable.DescPriceTable,
        InitialValidateDate = priceTable.InitialValidateDate,
        FinalValidateDate = priceTable.FinalValidateDate,
        SupplierId = priceTable.SupplierId,
        SupplierName = priceTable.Supplier?.Name ?? string.Empty,
        EnterpriseId = priceTable.EnterpriseId,
        UserId = priceTable.UserId,
        CreateDate = priceTable.CreateDate,
        AlteredDate = priceTable.AlteredDate
    };

    public List<PriceTableTableReturn> MainToTableList(List<PriceTable> priceTables) =>
        priceTables.Select(p => new PriceTableTableReturn
        {
            Id = p.Id,
            DescPriceTable = p.DescPriceTable,
            InitialValidateDate = p.InitialValidateDate,
            FinalValidateDate = p.FinalValidateDate,
            SupplierId = p.SupplierId,
            Supplier = p.Supplier?.Name ?? string.Empty,
            CreateDate = p.CreateDate
        }).ToList();
}
