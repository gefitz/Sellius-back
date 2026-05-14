using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface IGroupCustomerMapper
{
    GroupCustomer DtoRegisterToMain(GroupCustomerRegister dto, Guid enterpriseId);
    GroupCustomerEdit MainToDtoEdit(GroupCustomer groupCustomer);
    List<GroupCustomerTableReturn> MainToTableList(List<GroupCustomer> groupCustomers);
}
