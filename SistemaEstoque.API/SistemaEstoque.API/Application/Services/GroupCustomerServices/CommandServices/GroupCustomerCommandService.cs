using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.GroupCustomerServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.Cliente.Interfaces;

namespace Sellius.API.Application.Services.GroupCustomerServices.CommandServices;

public sealed class GroupCustomerCommandService(
    IGroupCustomerRepository repository,
    IGroupCustomerMapper mapper) : IGroupCustomerCommandService
{
    public async Task<bool> CreateGroupCustomer(GroupCustomerRegister dto, Guid enterpriseId)
    {
        var groupCustomer = mapper.DtoRegisterToMain(dto, enterpriseId);
        groupCustomer.CreateDate = DateTime.UtcNow;
        groupCustomer.AlteredDate = DateTime.UtcNow;
        groupCustomer.Active = 1;

        return await repository.CreateGroupCustomerAsync(groupCustomer);
    }

    public async Task<bool> UpdateGroupCustomer(GroupCustomerRegister dto)
    {
        var groupCustomer = await repository.FindByPredicateAsync(g => g.Id == dto.Id);

        if (groupCustomer is null)
            return false;

        groupCustomer.Name = dto.Name;
        groupCustomer.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateGroupCustomerAsync(groupCustomer);
    }

    public async Task<bool> InactiveGroupCustomer(long groupCustomerId)
    {
        var groupCustomer = await repository.FindByPredicateAsync(g => g.Id == groupCustomerId);

        if (groupCustomer is null)
            return false;

        groupCustomer.Active = 0;
        groupCustomer.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateGroupCustomerAsync(groupCustomer);
    }
}
