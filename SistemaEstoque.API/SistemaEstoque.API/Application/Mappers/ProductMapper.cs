using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityProduct;

namespace Sellius.API.Application.Mappers;

public sealed class ProductMapper : IProductMapper
{
    public Product DtoRegisterToMain(ProductRegister dto) =>
        new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            TypeProductId = dto.TypeProduct,
            Active = (short)dto.Active,
            CreateDate = dto.CreateDate == default ? DateTime.UtcNow : dto.CreateDate,
            AlteredDate = dto.AlteredDate
        };

    public ProductEdit MainToDtoEdit(Product product) =>
        new ProductEdit
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            TypeProductId = product.TypeProductId,
            Active = product.Active,
            SupplierId = product.SupplierId,
            CreateDate = product.CreateDate,
            AlteredDate = product.AlteredDate,
            EnterpriseId = product.EnterpriseId
        };

    public List<ProductTableReturn> MainToTableList(List<Product> products) =>
        products.Select(p => new ProductTableReturn
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description ?? string.Empty,
            TypeProduct = p.TypeProduct?.Name ?? string.Empty,
            TpProductId = p.TypeProductId ?? 0,
            CreateDate = p.CreateDate,
            AlteredDate = p.AlteredDate,
            Active = p.Active,
            Supplier = p.Supplier?.Name ?? string.Empty,
            SupplierId = p.SupplierId
        }).ToList();
}
