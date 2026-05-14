using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.StateServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.CidadeEstado.Interfaces;

namespace Sellius.API.Application.Services.StateServices.QueryServices;

public sealed class StateQueryService(
    IStateRepository repository,
    IStateMapper mapper) : IStateQueryService
{
    public async Task<StateEdit> FindByStateId(long stateId)
    {
        var state = await repository.FindByPredicateAsync(s => s.Id == stateId);
        return state is not null ? mapper.MainToDtoEdit(state) : new StateEdit();
    }

    public async Task<List<StateTableReturn>> FindAllStates(StateFilter filter)
    {
        var states = await repository.FindAllAsync(
            s => (filter.Name == null || s.Name.Contains(filter.Name))
                 && (filter.Acronym == null || s.Acronym.Contains(filter.Acronym)),
            null,
            o => o.OrderBy(s => s.Name));

        return mapper.MainToTableList(states);
    }
}
