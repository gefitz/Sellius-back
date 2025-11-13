using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Fornecedor.Interfaces;

namespace Sellius.API.Services
{
    public class FornecedorService
    {
        private readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<FornecedorDTO>> CadastrarFornecedor(FornecedorDTO dto)
        {
            dto.fAtivo = 1;
            FornecedoresModel model = dto;
            if (await _repository.Create(model))
                return Response<FornecedorDTO>.Ok(model);
            return Response<FornecedorDTO>.Failed("Falha ao criar um novo tipo produto");

        }
        public async Task<Response<PaginacaoTabelaResult<FornecedorTabelaResult, FiltroFornecedor>>> BuscarFornecedor(PaginacaoTabelaResult<FornecedorTabelaResult, FiltroFornecedor> dto)
        {
            try
            {

                PaginacaoTabelaResult<FornecedoresModel, FornecedoresModel> model = new PaginacaoTabelaResult<FornecedoresModel, FornecedoresModel>
                {
                    TotalRegistros = dto.TotalRegistros,
                    TotalPaginas = dto.TotalPaginas,
                    TamanhoPagina = dto.TamanhoPagina,
                    Filtro = dto.Filtro,
                    PaginaAtual = dto.PaginaAtual

                };
                var result = await _repository.Filtrar(model);
                dto.Dados = FornecedorTabelaResult.FromList(result.Dados);
                return Response<PaginacaoTabelaResult<FornecedorTabelaResult, FiltroFornecedor>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<FornecedorTabelaResult, FiltroFornecedor>>.Failed(ex.Message);
            }
        }
        public async Task<Response<FornecedorDTO>> BuscarId(int id)
        {
            try
            {
                FornecedoresModel cliente = new FornecedoresModel { id = id };
                return Response<FornecedorDTO>.Ok(await _repository.BuscaDireto(cliente));
            }
            catch (Exception ex)
            {
                return Response<FornecedorDTO>.Failed(ex.Message);
            }
        }
        public async Task<Response<FornecedorDTO>> UpdateFornecedor(FornecedorDTO dto)
        {

            var fornecedorOriginal = await BuscarId(dto.id);
            if (!fornecedorOriginal.success)
                return Response<FornecedorDTO>.Failed("Fornecedor não foi encontrado");
            

            FornecedoresModel model = dto;
            model.dthAlteracao = DateTime.UtcNow;
            model.dthCadastro = (DateTime)fornecedorOriginal.Data.dthCadastro;
            if(fornecedorOriginal.Data.cidade != null)
            model.Cidade = fornecedorOriginal.Data.cidade;

            if (await _repository.Update(model))
                return Response<FornecedorDTO>.Ok(model);
            return Response<FornecedorDTO>.Failed("Falha ao fazer modificação");
        }
        public async Task<Response<FornecedorDTO>> InativarFornecedor(FornecedorDTO dto)
        {
            var model = await BuscarId(dto.id);
            if (!model.success)
            {
                return Response<FornecedorDTO>.Failed("O id desse tipo produto não foi encontrado");
            }
            return await UpdateFornecedor(dto);
        }
        public async Task<Response<List<FornecedorDTO>>>CarregarComboFornecedor(FornecedorDTO fornecedor)
        {
            var fornecedoRet = await _repository.CarregarComboFornecedor(fornecedor);
            return Response<List<FornecedorDTO>>.Ok(FornecedorDTO.FromList(fornecedoRet));
        }
    }
}
