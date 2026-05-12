using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Application.Services.TypeUserServices.QueryServices.Interfaces;

public interface ITypeUserQueryService
{
    Task<TypeUserEdit> FindByTypeUserId(long typeUserId);
    Task<List<TypeUserRegister>> FindAllTypeUsers(TypeUserFilter filter, Guid enterpriseId);
}
