using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Application.Mappers;

public sealed class CityMapper : ICityMapper
{
    public City DtoRegisterToMain(CityRegister dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        StateId = dto.StateId
    };

    public CityEdit MainToDtoEdit(City city) => new()
    {
        Id = city.Id,
        Name = city.Name,
        StateId = city.StateId,
        StateName = city.State?.Name ?? string.Empty
    };

    public List<CityTableReturn> MainToTableList(List<City> cities) =>
        cities.Select(c => new CityTableReturn
        {
            Id = c.Id,
            Name = c.Name,
            StateId = c.StateId,
            State = c.State?.Name ?? string.Empty
        }).ToList();
}
