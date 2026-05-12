using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.UserServices.QueryServices.Interfaces;
using Sellius.API.Domain.Models;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.UserServices.QueryServices;

public sealed class UserQueryService(IUserRepository repository,IUserMapper mapper) : IUserQueryService
{
    public async Task<UserEdit> FindByUserId(Guid userId)
    {
        var user = await repository.FindPredicateUserAsync(u => u.Id == userId);

        return user is not null ? mapper.MainToDtoEdit(user) : new UserEdit();
    }

    public async Task<PaginationTableResult<UserTable>> FindAllUser(UserFilter filter,Guid enterpriseId)
    {
        var users = await repository.FindAllUsers(filter, enterpriseId,null, 
            o => o.OrderBy(u => u.Name));

        return users.Dados is not null ? mapper.MainToPaginationTable(users) : new PaginationTableResult<UserTable>();
    }
}