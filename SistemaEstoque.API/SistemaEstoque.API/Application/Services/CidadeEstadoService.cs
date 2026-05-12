using Sellius.API.Application.DTOs;
using Sellius.API.Domain.Entity;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Services
{
    public class CidadeEstadoService
    {
        private readonly IDbMethods<City> cidadeDb;
        private readonly IDbMethods<State> estadoDb;

        public CidadeEstadoService(IDbMethods<City> cidadeDb, IDbMethods<State> estadoDb)
        {
            this.cidadeDb = cidadeDb;
            this.estadoDb = estadoDb;
        }
        public async Task<Response<IEnumerable<StateDTO>>> buscarEstados()
        {
            try
            {
                IEnumerable<StateDTO> estado = StateDTO.toList(await estadoDb.Filtrar(null));
                return Response<IEnumerable<StateDTO>>.Ok(estado);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<StateDTO>>.Failed(ex.Message);
            }
           
        }
        public async Task<Response<IEnumerable<CityDTO>>> BuscarCidade(int idEstado)
        {
            try
            {
                IEnumerable<CityDTO> estado = CityDTO.toList(await cidadeDb.Filtrar(new City { EstadoId = idEstado}));
                return Response<IEnumerable<CityDTO>>.Ok(estado);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<CityDTO>>.Failed(ex.Message);
            }

        }
    }
}
