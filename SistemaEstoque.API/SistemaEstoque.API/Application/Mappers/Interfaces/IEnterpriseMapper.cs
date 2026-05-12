using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityEnterprises;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface IEnterpriseMapper
{
    Enterprise DtoRegisterToMain(EnterpriseRegister dto);
    EnterpriseEdit MainToDtoEdit(Enterprise enterprise);
}
