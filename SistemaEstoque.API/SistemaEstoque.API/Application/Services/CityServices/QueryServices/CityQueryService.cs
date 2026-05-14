using Microsoft.EntityFrameworkCore;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.CityServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.CidadeEstado.Interfaces;

namespace Sellius.API.Application.Services.CityServices.QueryServices;

public sealed class CityQueryService(
    ICityRepository repository,
    ICityMapper mapper) : ICityQueryService
{
    public async Task<CityEdit> FindByCityId(long cityId)
    {
        var city = await repository.FindByPredicateAsync(
            c => c.Id == cityId,
            q => q.Include(c => c.State));

        return city is not null ? mapper.MainToDtoEdit(city) : new CityEdit();
    }

    public async Task<List<CityTableReturn>> FindAllCities(CityFilter filter)
    {
        var cities = await repository.FindAllAsync(
            c => (filter.Name == null || c.Name.Contains(filter.Name))
                 && (filter.StateId <= 0 || c.StateId == filter.StateId),
            q => q.Include(c => c.State),
            o => o.OrderBy(c => c.Name));

        return mapper.MainToTableList(cities);
    }
}
