using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.ProductServices.CommandServices.Interfaces;

public interface IProductCommandServices
{
    Task<bool> CreateProduct(ProductRegister dto, Guid enterpriseId);
    Task<bool> UpdateProduct(ProductRegister dto);
    Task<bool> InactiveProduct(long productId);
}