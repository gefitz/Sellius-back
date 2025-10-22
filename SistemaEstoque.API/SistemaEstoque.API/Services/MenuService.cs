using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Menu.Interface;

namespace Sellius.API.Services
{
    public class MenuService
    {

        private readonly IMenuRepository _repository;

        public MenuService(IMenuRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<MenuDTO>> CriarMenu(MenuDTO dto)
        {
            try
            {
                MenuModel menu = dto;
                menu.DtCadastro = DateTime.UtcNow;
                menu.FAtivo = true;

                if (await _repository.Create(menu))
                {
                    dto = menu;
                    return Response<MenuDTO>.Ok(dto);
                }
                return Response<MenuDTO>.Failed("Falha ao cadastrar o menu.");
            }
            catch (Exception ex)
            {
                return Response<MenuDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<MenuDTO>> UpdateMenu(MenuDTO dto)
        {
            try
            {
                MenuModel model = dto;
                model.DtAtualizacao = DateTime.UtcNow;

                if (await _repository.Update(model))
                {
                    return Response<MenuDTO>.Ok(model);
                }
                return Response<MenuDTO>.Failed("Falha ao tentar atualizar o menu.");
            }
            catch (Exception ex)
            {
                return Response<MenuDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<MenuDTO>> BuscaMenuPorId(int id)
        {
            try
            {
                MenuModel menu = new MenuModel { Id = id };
                menu = await _repository.BuscaDireto(menu);

                if (menu != null)
                    return Response<MenuDTO>.Ok(menu);

                return Response<MenuDTO>.Failed("Menu não localizado.");
            }
            catch (Exception ex)
            {
                return Response<MenuDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<MenuDTO>> InativarMenu(int id)
        {
            try
            {
                var responseBusca = await BuscaMenuPorId(id);
                if (!responseBusca.success)
                    return responseBusca;

                responseBusca.Data.FAtivo = false;
                return await UpdateMenu(responseBusca.Data);
            }
            catch (Exception ex)
            {
                return Response<MenuDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<PaginacaoTabelaResult<MenuDTO, MenuDTO>>> ObterTodosMenus(PaginacaoTabelaResult<MenuDTO, MenuDTO> paginacao)
        {
            try
            {
                PaginacaoTabelaResult<MenuModel, MenuModel> modelPaginacao = new PaginacaoTabelaResult<MenuModel, MenuModel>
                {
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    TotalRegistros = paginacao.TotalRegistros,
                };
                modelPaginacao = await _repository.Filtrar(modelPaginacao);
                paginacao.Dados = MenuDTO.FromList(modelPaginacao.Dados);
                return Response<PaginacaoTabelaResult<MenuDTO, MenuDTO>>.Ok(paginacao);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<MenuDTO, MenuDTO>>.Failed(ex.Message);
            }
        }
    }
}
