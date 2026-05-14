using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityProduct;

namespace Sellius.API.Application.Mappers;

public sealed class TypeProductMapper : ITypeProductMapper
{
    public TypeProduct DtoRegisterToMain(TypeProductRegister dto, Guid enterpriseId) => new()
    {
        Name = dto.Name,
        Description = dto.Description,
        Active = dto.Active,
        EnterpriseId = enterpriseId
    };

    public TypeProductEdit MainToDtoEdit(TypeProduct typeProduct) => new()
    {
        Id = typeProduct.Id,
        Name = typeProduct.Name,
        Description = typeProduct.Description,
        Active = typeProduct.Active,
        EnterpriseId = typeProduct.EnterpriseId
    };

    public List<TypeProductTableReturn> MainToTableList(List<TypeProduct> typeProducts) =>
        typeProducts.Select(t => new TypeProductTableReturn
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            Active = t.Active
        }).ToList();
}
