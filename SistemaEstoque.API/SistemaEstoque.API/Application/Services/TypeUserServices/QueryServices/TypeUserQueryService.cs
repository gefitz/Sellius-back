using Microsoft.EntityFrameworkCore;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.TypeUserServices.QueryServices.Interfaces;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.TypeUserServices.QueryServices;

public sealed class TypeUserQueryService(
    ITpUserRepository repository,
    ITypeUserMapper mapper) : ITypeUserQueryService
{
    public async Task<TypeUserEdit> FindByTypeUserId(long typeUserId)
    {
        var typeUser = await repository.FindByPredicateAsync(
            t => t.Id == typeUserId,
            q => q.Include(t => t.TypeUserXMenus)
                  .Include(t => t.UserConfiguration)!,
            true);

        return typeUser is not null ? mapper.MainToDtoEdit(typeUser) : new TypeUserEdit();
    }

    public async Task<List<TypeUserRegister>> FindAllTypeUsers(TypeUserFilter filter, Guid enterpriseId)
    {
        var typeUsers = await repository.FindAllAsync(
            t => t.EnterpriseId == enterpriseId
                 && (filter.Name == null || t.NameType.Contains(filter.Name))
                 && (filter.Active < 0 || t.Active == filter.Active),
            null,
            o => o.OrderBy(t => t.NameType));

        return mapper.MainToList(typeUsers);
    }
}
