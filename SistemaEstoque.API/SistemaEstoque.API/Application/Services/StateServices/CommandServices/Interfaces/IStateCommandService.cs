using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.StateServices.CommandServices.Interfaces;

public interface IStateCommandService
{
    Task<bool> CreateState(StateRegister dto);
    Task<bool> UpdateState(StateRegister dto);
}
