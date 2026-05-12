using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;

namespace Sellius.API.Application.Services.CustomerServices.CommandServices.Interfaces;

public interface ICustomerCommandService
{
    Task<bool> CreateCustomer(CustomerRegister dto, Guid enterpriseId);
    Task<bool> UpdateCustomer(CustomerRegister dto);
    Task<bool> InactiveCustomer(long customerId);
}
