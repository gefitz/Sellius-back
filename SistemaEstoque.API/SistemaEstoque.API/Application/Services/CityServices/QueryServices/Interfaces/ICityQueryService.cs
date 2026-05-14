using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.CityServices.QueryServices.Interfaces;

public interface ICityQueryService
{
    Task<CityEdit> FindByCityId(long cityId);
    Task<List<CityTableReturn>> FindAllCities(CityFilter filter);
}
