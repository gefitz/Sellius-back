using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Models;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface IUserMapper
{
    User DtoRegisterToMain(UserRegister dtoRegister);
    UserEdit MainToDtoEdit(User user);
    PaginationTableResult<UserTable> MainToPaginationTable(PaginationTableResult<User> paginationTable);
}