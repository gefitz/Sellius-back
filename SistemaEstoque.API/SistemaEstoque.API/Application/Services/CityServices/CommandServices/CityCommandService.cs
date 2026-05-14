using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.CityServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.CidadeEstado.Interfaces;

namespace Sellius.API.Application.Services.CityServices.CommandServices;

public sealed class CityCommandService(
    ICityRepository repository,
    ICityMapper mapper) : ICityCommandService
{
    public async Task<bool> CreateCity(CityRegister dto)
    {
        var city = mapper.DtoRegisterToMain(dto);
        return await repository.CreateCityAsync(city);
    }

    public async Task<bool> UpdateCity(CityRegister dto)
    {
        var city = await repository.FindByPredicateAsync(c => c.Id == dto.Id);

        if (city is null)
            return false;

        city.Name = dto.Name;
        city.StateId = dto.StateId;

        return await repository.UpdateCityAsync(city);
    }
}
