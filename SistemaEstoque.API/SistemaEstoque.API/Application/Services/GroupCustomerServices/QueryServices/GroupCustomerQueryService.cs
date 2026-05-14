using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.GroupCustomerServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Cliente.Interfaces;

namespace Sellius.API.Application.Services.GroupCustomerServices.QueryServices;

public sealed class GroupCustomerQueryService(
    IGroupCustomerRepository repository,
    IGroupCustomerMapper mapper) : IGroupCustomerQueryService
{
    public async Task<GroupCustomerEdit> FindByGroupCustomerId(long groupCustomerId)
    {
        var groupCustomer = await repository.FindByPredicateAsync(g => g.Id == groupCustomerId);
        return groupCustomer is not null ? mapper.MainToDtoEdit(groupCustomer) : new GroupCustomerEdit();
    }

    public async Task<List<GroupCustomerTableReturn>> FindAllGroupCustomers(GroupCustomerFilter filter, Guid enterpriseId)
    {
        var groupCustomers = await repository.FindAllAsync(
            g => g.EnterpriseId == enterpriseId
                 && (filter.Name == null || g.Name.Contains(filter.Name))
                 && (filter.Active < 0 || g.Active == filter.Active),
            null,
            o => o.OrderBy(g => g.Name));

        return mapper.MainToTableList(groupCustomers);
    }
}
