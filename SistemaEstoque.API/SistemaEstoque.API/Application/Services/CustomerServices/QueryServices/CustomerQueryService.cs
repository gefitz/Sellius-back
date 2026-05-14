using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.CustomerServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Cliente.Interfaces;

namespace Sellius.API.Application.Services.CustomerServices.QueryServices;

public sealed class CustomerQueryService(
    ICustomerRepository repository,
    ICustomerMapper mapper) : ICustomerQueryService
{
    public async Task<CustomerEdit> FindByCustomerId(long customerId)
    {
        var customer = await repository.FindByPredicateAsync(
            c => c.Id == customerId);

        return customer is not null ? mapper.MainToDtoEdit(customer) : new CustomerEdit();
    }

    public async Task<List<CustomerTableReturn>> FindAllCustomers(CustomerFilter filter, Guid enterpriseId)
    {
        var customers = await repository.FindAllAsync(
            c => c.EnterpriseId == enterpriseId
                 && (filter.Name == null || c.Name.Contains(filter.Name))
                 && (filter.Document == null || c.Document.Contains(filter.Document))
                 && (filter.CityId <= 0 || c.CityId == filter.CityId)
                 && (filter.Active < 0 || c.Active == filter.Active),
            null,
            o => o.OrderBy(c => c.Name));

        return mapper.MainToTableList(customers);
    }
}
