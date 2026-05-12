using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface ICustomerMapper
{
    Customer DtoRegisterToMain(CustomerRegister dto, Guid enterpriseId);
    CustomerEdit MainToDtoEdit(Customer customer);
    List<CustomerTableReturn> MainToTableList(List<Customer> customers);
}
