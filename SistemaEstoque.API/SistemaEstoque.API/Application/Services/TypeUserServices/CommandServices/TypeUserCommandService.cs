using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.TypeUserServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.TypeUserServices.CommandServices;

public sealed class TypeUserCommandService(
    ITpUserRepository repository,
    ITypeUserMapper mapper) : ITypeUserCommandService
{
    public async Task<bool> CreateTypeUser(TypeUserRegister dto)
    {
        if (await TypeUserAlreadyExists(dto.Name, dto.EnterpriseId))
            return false;

        var typeUser = mapper.DtoRegisterToMain(dto);
        typeUser.CreateDate = DateTime.UtcNow;
        typeUser.AlteredDate = DateTime.UtcNow;
        typeUser.Active = 1;

        return await repository.CreateTpUserAsync(typeUser);
    }

    public async Task<bool> UpdateTypeUser(TypeUserRegister dto)
    {
        var typeUser = await repository.FindByPredicateAsync(
            t => t.Id == dto.Id, null, false);

        if (typeUser is null)
            return false;

        typeUser.NameType = dto.Name;
        typeUser.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateTpUserAsync(typeUser);
    }

    public async Task<bool> InactiveTypeUser(long typeUserId)
    {
        var typeUser = await repository.FindByPredicateAsync(
            t => t.Id == typeUserId, null, false);

        if (typeUser is null)
            return false;

        typeUser.Active = 0;
        typeUser.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateTpUserAsync(typeUser);
    }

    private async Task<bool> TypeUserAlreadyExists(string name, Guid enterpriseId)
    {
        var existing = await repository.FindByPredicateAsync(
            t => t.NameType == name && t.EnterpriseId == enterpriseId, null, true);

        return existing is not null;
    }
}
