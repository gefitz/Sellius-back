using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.StateServices.QueryServices.Interfaces;

public interface IStateQueryService
{
    Task<StateEdit> FindByStateId(long stateId);
    Task<List<StateTableReturn>> FindAllStates(StateFilter filter);
}
