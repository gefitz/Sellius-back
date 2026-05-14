using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Models;

namespace Sellius.API.Application.Mappers;

public sealed class UserMapper : IUserMapper
{
    public User DtoRegisterToMain(UserRegister dtoRegister) =>
        new User
        {
            Id = dtoRegister.Id == Guid.Empty ? Guid.NewGuid() : dtoRegister.Id,
            Name = dtoRegister.Name,
            Document = dtoRegister.Document,
            Email = dtoRegister.Email,
            ZipCode = dtoRegister.ZipCode,
            Street = dtoRegister.Street,
            CreateDate = dtoRegister.DateCreate == default ? DateTime.UtcNow : dtoRegister.DateCreate,
            AlteredDate = DateTime.UtcNow,
            TypeUserId = dtoRegister.TypeUser,
            Active = dtoRegister.Active,
            EnterpriseId = dtoRegister.EnterpriseId,
            CityId = dtoRegister.CityId
        };

    public UserEdit MainToDtoEdit(User user) =>
        new UserEdit
        {
            Id = user.Id,
            Name = user.Name,
            Document = user.Document,
            Email = user.Email,
            ZipCode = user.ZipCode,
            Street = user.Street,
            CreateDate = user.CreateDate,
            AlteredDate = user.AlteredDate,
            TypeUserId = user.TypeUserId,
            Active = user.Active,
            EnterpriseId = user.EnterpriseId,
            CityId = user.CityId
        };

    public PaginationTableResult<UserTable> MainToPaginationTable(PaginationTableResult<User> paginationTable) =>
        new PaginationTableResult<UserTable>
        {
            CurrentPage = paginationTable.CurrentPage,
            TotalPage = paginationTable.TotalPage,
            TotalRecords = paginationTable.TotalRecords,
            PageSize = paginationTable.PageSize,
            Dados = paginationTable.Dados?.Select(u => new UserTable
            {
                Name = u.Name,
                Email = u.Email,
                TypeUser = u.TypeUser?.NameType ?? string.Empty,
                City = u.City?.Name ?? string.Empty,
                Address = u.Street,
                Active = u.Active,
                CreateDate = u.CreateDate,
                AlteredDate = u.AlteredDate
            }).ToList()
        };
}
