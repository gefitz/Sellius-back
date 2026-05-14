using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Application.Mappers;

public sealed class StateMapper : IStateMapper
{
    public State DtoRegisterToMain(StateRegister dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Acronym = dto.Acronym
    };

    public StateEdit MainToDtoEdit(State state) => new()
    {
        Id = state.Id,
        Name = state.Name,
        Acronym = state.Acronym
    };

    public List<StateTableReturn> MainToTableList(List<State> states) =>
        states.Select(s => new StateTableReturn
        {
            Id = s.Id,
            Name = s.Name,
            Acronym = s.Acronym
        }).ToList();
}
