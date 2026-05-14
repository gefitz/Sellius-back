using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.GroupCustomerServices.CommandServices.Interfaces;

public interface IGroupCustomerCommandService
{
    Task<bool> CreateGroupCustomer(GroupCustomerRegister dto, Guid enterpriseId);
    Task<bool> UpdateGroupCustomer(GroupCustomerRegister dto);
    Task<bool> InactiveGroupCustomer(long groupCustomerId);
}
