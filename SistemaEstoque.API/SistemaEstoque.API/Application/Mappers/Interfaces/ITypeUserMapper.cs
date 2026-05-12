using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface ITypeUserMapper
{
    TypeUser DtoRegisterToMain(TypeUserRegister dto);
    TypeUserEdit MainToDtoEdit(TypeUser typeUser);
    List<TypeUserRegister> MainToList(List<TypeUser> typeUsers);
}
