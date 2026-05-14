using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface ICityMapper
{
    City DtoRegisterToMain(CityRegister dto);
    CityEdit MainToDtoEdit(City city);
    List<CityTableReturn> MainToTableList(List<City> cities);
}
