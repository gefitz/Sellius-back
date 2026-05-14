using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.CityServices.CommandServices.Interfaces;

public interface ICityCommandService
{
    Task<bool> CreateCity(CityRegister dto);
    Task<bool> UpdateCity(CityRegister dto);
}
