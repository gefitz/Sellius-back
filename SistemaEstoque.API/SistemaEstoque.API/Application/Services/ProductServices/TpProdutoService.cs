using AutoMapper;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Models;
using Sellius.API.Domain.Models.Produto;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Produto;
using Sellius.API.Repository.Produto.Interface;

namespace Sellius.API.Services.Produtos
{
    public class TpProdutoService
    {
        private readonly TpProdutoRepository _repository;

        public TpProdutoService(TpProdutoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<TypeProductRegister>> CadastrarTpProduto(TypeProductRegister dto)
        {
            TypeProductRegister model = dto;
            try
            {

                if (await _repository.Create(model))
                    return Response<TypeProductRegister>.Ok(model);
                return Response<TypeProductRegister>.Failed("Falha ao criar um novo tipo produto");
            }catch(Exception ex)
            {
                return Response<TypeProductRegister>.Failed(ex.Message);
            }

        }
        public async Task<Response<PaginationTableResult<>>> BuscarTpProudo(PaginationTableResult<> dto)
        {
            try
            {

                PaginationTableResult<> model = new PaginationTableResult<>
                {
                    TotalRegistros = dto.TotalRegistros,
                    TotalPaginas = dto.TotalPaginas,
                    TamanhoPagina = dto.TamanhoPagina,
                    Filtro = dto.Filtro,
                    PaginaAtual = dto.PaginaAtual

                };
                var result = await _repository.Filtrar(model);
                dto.Dados = TypeProductRegister.FromList(result.Dados);
                dto.TotalRegistros = result.TotalRegistros;
                return Response<PaginationTableResult<>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }
        }
        public async Task<Response<TypeProductRegister>> BuscarId(int id)
        {
            try
            {
                TypeProduct tp = new TypeProduct { id = id };
                return Response<TypeProductRegister>.Ok(await _repository.BuscaDireto(tp));
            }
            catch (Exception ex)
            {
                return Response<TypeProductRegister>.Failed(ex.Message);
            }
        }
        public async Task<Response<TypeProductRegister>> UpdateTpProduto(TypeProductRegister dto)
        {
            TypeProduct model = dto;
            if (await _repository.Update(model))
                return Response<TypeProductRegister>.Ok(model);
            return Response<TypeProductRegister>.Failed("Falha ao fazer modificação");
        }
        public async Task<Response<TypeProductRegister>> InativarTpProduto(int id)
        {
            var model = await BuscarId(id);
            if (!model.success)
            {
                return Response<TypeProductRegister>.Failed("O id desse tipo produto não foi encontrado");
            }
            TypeProduct tp = model.Data;
            tp.fAtivo = 0;
            if (await _repository.Delete(tp))
            {
                return Response<TypeProductRegister>.Ok(tp);
            }
            return Response<TypeProductRegister>.Failed("Falha ao inativar o tipo produto");
        }
        public async Task<Response<List<TypeProductRegister>>> CarregarCombo(int idEmpresa)
        {
            List<TypeProductRegister> listTp = TypeProductRegister.FromList(await _repository.CarregarCombo(idEmpresa));
            return Response<List<TypeProductRegister>>.Ok(listTp);
        }
    }
}
