using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.CustomerServices.QueryServices.Interfaces;

public interface ICustomerQueryService
{
    Task<CustomerEdit> FindByCustomerId(long customerId);
    Task<List<CustomerTableReturn>> FindAllCustomers(CustomerFilter filter, Guid enterpriseId);
}
