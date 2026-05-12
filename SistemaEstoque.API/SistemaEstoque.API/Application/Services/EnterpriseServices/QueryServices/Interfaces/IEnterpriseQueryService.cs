using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.EnterpriseServices.QueryServices.Interfaces;

public interface IEnterpriseQueryService
{
    Task<EnterpriseEdit> FindByEnterpriseId(Guid enterpriseId);
    Task<List<EnterpriseRegister>> FindAllEnterprises();
}
