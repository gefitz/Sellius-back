using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.GroupCustomerServices.QueryServices.Interfaces;

public interface IGroupCustomerQueryService
{
    Task<GroupCustomerEdit> FindByGroupCustomerId(long groupCustomerId);
    Task<List<GroupCustomerTableReturn>> FindAllGroupCustomers(GroupCustomerFilter filter, Guid enterpriseId);
}
