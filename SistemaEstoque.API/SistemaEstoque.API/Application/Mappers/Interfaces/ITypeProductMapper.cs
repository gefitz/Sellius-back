using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityProduct;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface ITypeProductMapper
{
    TypeProduct DtoRegisterToMain(TypeProductRegister dto, Guid enterpriseId);
    TypeProductEdit MainToDtoEdit(TypeProduct typeProduct);
    List<TypeProductTableReturn> MainToTableList(List<TypeProduct> typeProducts);
}
