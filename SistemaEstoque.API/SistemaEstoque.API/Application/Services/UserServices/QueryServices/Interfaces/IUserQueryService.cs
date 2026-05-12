using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Models;

namespace Sellius.API.Application.Services.UserServices.QueryServices.Interfaces;

public interface IUserQueryService
{
    Task<UserEdit> FindByUserId(Guid userId);
    Task<PaginationTableResult<UserTable>> FindAllUser(UserFilter filter,Guid enterpriseId);
}