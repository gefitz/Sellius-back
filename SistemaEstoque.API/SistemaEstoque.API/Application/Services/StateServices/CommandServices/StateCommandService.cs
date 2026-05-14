using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.StateServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.CidadeEstado.Interfaces;

namespace Sellius.API.Application.Services.StateServices.CommandServices;

public sealed class StateCommandService(
    IStateRepository repository,
    IStateMapper mapper) : IStateCommandService
{
    public async Task<bool> CreateState(StateRegister dto)
    {
        var state = mapper.DtoRegisterToMain(dto);
        return await repository.CreateStateAsync(state);
    }

    public async Task<bool> UpdateState(StateRegister dto)
    {
        var state = await repository.FindByPredicateAsync(s => s.Id == dto.Id);

        if (state is null)
            return false;

        state.Name = dto.Name;
        state.Acronym = dto.Acronym;

        return await repository.UpdateStateAsync(state);
    }
}
