using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.Services.CityServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.CityServices.QueryServices.Interfaces;
using Sellius.API.Application.Services.StateServices.QueryServices.Interfaces;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api")]
    public class StateAndCityController(IStateQueryService stateQueryService, ICityQueryService queryService) : Controller
    {
        [HttpGet("states")]
        public async Task<IActionResult> GetAllStates([FromQuery] StateFilter filter)
        {
            var result = await stateQueryService.FindAllStates(filter);
            return Ok(result);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities([FromQuery] CityFilter cityFilter)
        {
            return Ok(await queryService.FindAllCities(cityFilter));
        }
    }
}
