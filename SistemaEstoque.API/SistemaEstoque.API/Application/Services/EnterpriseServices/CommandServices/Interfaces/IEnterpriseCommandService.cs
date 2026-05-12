using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.EnterpriseServices.CommandServices.Interfaces;

public interface IEnterpriseCommandService
{
    Task<bool> CreateEnterprise(EnterpriseRegister dto);
    Task<bool> UpdateEnterprise(EnterpriseRegister dto);
    Task<bool> InactiveEnterprise(Guid enterpriseId);
}
