using Sellius.API.DTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Services
{
    public class CidadeEstadoService
    {
        private readonly IDbMethods<CidadeModel> cidadeDb;
        private readonly IDbMethods<EstadoModel> estadoDb;

        public CidadeEstadoService(IDbMethods<CidadeModel> cidadeDb, IDbMethods<EstadoModel> estadoDb)
        {
            this.cidadeDb = cidadeDb;
            this.estadoDb = estadoDb;
        }
        public async Task<Response<IEnumerable<EstadoDTO>>> buscarEstados()
        {
            try
            {
                IEnumerable<EstadoDTO> estado = EstadoDTO.toList(await estadoDb.Filtrar(null));
                return Response<IEnumerable<EstadoDTO>>.Ok(estado);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<EstadoDTO>>.Failed(ex.Message);
            }
           
        }
        public async Task<Response<IEnumerable<CidadeDTO>>> BuscarCidade(int idEstado)
        {
            try
            {
                IEnumerable<CidadeDTO> estado = CidadeDTO.toList(await cidadeDb.Filtrar(new CidadeModel { EstadoId = idEstado}));
                return Response<IEnumerable<CidadeDTO>>.Ok(estado);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<CidadeDTO>>.Failed(ex.Message);
            }

        }
    }
}
