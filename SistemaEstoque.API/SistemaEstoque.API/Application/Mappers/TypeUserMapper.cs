using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using DtoUserConfig = Sellius.API.Application.DTOs.RegisterDTOs.UserConfiguration;
using EntityUserConfig = Sellius.API.Domain.Entity.EntityUsers.UserConfiguration;

namespace Sellius.API.Application.Mappers;

public sealed class TypeUserMapper : ITypeUserMapper
{
    public TypeUser DtoRegisterToMain(TypeUserRegister dto) =>
        new TypeUser
        {
            NameType = dto.Name,
            EnterpriseId = dto.EnterpriseId,
            CreateDate = dto.DateCreate == default ? DateTime.UtcNow : dto.DateCreate,
            AlteredDate = dto.AlteredDate == default ? DateTime.UtcNow : dto.AlteredDate,
            Active = dto.Active
        };

    public TypeUserEdit MainToDtoEdit(TypeUser typeUser) =>
        new TypeUserEdit
        {
            Id = typeUser.Id,
            Name = typeUser.NameType,
            EnterpriseId = typeUser.EnterpriseId,
            CreateDate = typeUser.CreateDate,
            AlteredDate = typeUser.AlteredDate,
            Active = typeUser.Active,
            MenuIds = typeUser.TypeUserXMenus?.Select(x => x.MenuId).ToList(),
            UserConfiguration = MapConfiguration(typeUser.TpUsuarioConfigurcao)
        };

    public List<TypeUserRegister> MainToList(List<TypeUser> typeUsers) =>
        typeUsers.Select(t => new TypeUserRegister
        {
            Id = t.Id,
            Name = t.NameType,
            EnterpriseId = t.EnterpriseId,
            DateCreate = t.CreateDate,
            AlteredDate = t.AlteredDate,
            Active = t.Active,
            MenuId = t.TypeUserXMenus?.Select(x => x.MenuId).ToList() ?? []
        }).ToList();

    private static DtoUserConfig? MapConfiguration(EntityUserConfig? config)
    {
        if (config is null) return null;

        return new DtoUserConfig
        {
            Id = config.TpUserId,
            PermissionCreate = config.PermissionCreate,
            PermissionDelete = config.PermissionDelete,
            PermissionEdit = config.PermissionEdit,
            PermissionInactive = config.PermissionInactivate,
            PermissionApprove = config.PermissionApprove,
            PermissionExport = config.PermissionExport,
            PermissionControlUser = config.PermissionControlUser
        };
    }
}
