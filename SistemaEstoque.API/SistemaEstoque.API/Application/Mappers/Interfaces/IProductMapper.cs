using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityProduct;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface IProductMapper
{
    Product DtoRegisterToMain(ProductRegister dto);
    ProductEdit MainToDtoEdit(Product product);
    List<ProductTableReturn> MainToTableList(List<Product> products);
}
