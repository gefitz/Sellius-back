using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Application.Mappers;

public sealed class GroupCustomerMapper : IGroupCustomerMapper
{
    public GroupCustomer DtoRegisterToMain(GroupCustomerRegister dto, Guid enterpriseId) => new()
    {
        Name = dto.Name,
        Active = dto.Active,
        EnterpriseId = enterpriseId,
        CreateDate = DateTime.UtcNow,
        AlteredDate = DateTime.UtcNow
    };

    public GroupCustomerEdit MainToDtoEdit(GroupCustomer groupCustomer) => new()
    {
        Id = groupCustomer.Id,
        Name = groupCustomer.Name,
        Active = groupCustomer.Active,
        EnterpriseId = groupCustomer.EnterpriseId,
        CreateDate = groupCustomer.CreateDate,
        AlteredDate = groupCustomer.AlteredDate
    };

    public List<GroupCustomerTableReturn> MainToTableList(List<GroupCustomer> groupCustomers) =>
        groupCustomers.Select(g => new GroupCustomerTableReturn
        {
            Id = g.Id,
            Name = g.Name,
            Active = g.Active,
            CreateDate = g.CreateDate,
            AlteredDate = g.AlteredDate
        }).ToList();
}
