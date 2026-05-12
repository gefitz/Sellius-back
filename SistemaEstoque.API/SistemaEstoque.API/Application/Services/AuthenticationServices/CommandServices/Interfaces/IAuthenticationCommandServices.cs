using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.AuthenticationServices.CommandServices.Interfaces;

public interface IAuthenticationCommandServices
{
    Task<bool> CreateLogin(LoginRegister register);
}