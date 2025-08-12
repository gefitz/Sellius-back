using Sellius.API.DTOs.TabelasDTOs;

namespace Sellius.API.Repository.Interfaces
{
    public interface IPaginacao<model,filtro>
    {
        public Task<PaginacaoTabelaResult<model, filtro>> Filtrar(PaginacaoTabelaResult<model, filtro> obj);

    }
}
