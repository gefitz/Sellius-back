using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Menu;
using Sellius.API.Repository.Menu.Interface;

namespace Sellius.API.Services
{
    public class MenuService
    {

        private readonly MenuRepository _repository;

        public MenuService(MenuRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<MenuDTO>> CriarMenu(MenuDTO dto)
        {
            try
            {
                MenuModel menu = dto;
                menu.DtCadastro = DateTime.UtcNow;
                menu.FAtivo = 1;

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

                responseBusca.Data.FAtivo = 0;
                return await UpdateMenu(responseBusca.Data);
            }
            catch (Exception ex)
            {
                return Response<MenuDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<PaginacaoTabelaResult<MenuDTO, MenuFiltro>>> ObterTodosMenus(PaginacaoTabelaResult<MenuDTO, MenuFiltro> paginacao)
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
                //paginacao.Dados = MenuDTO.FromList(modelPaginacao.Dados);
                paginacao = new PaginacaoTabelaResult<MenuDTO, MenuFiltro>
                {
                    PaginaAtual = modelPaginacao.PaginaAtual,
                    TamanhoPagina = modelPaginacao.TamanhoPagina,
                    TotalPaginas = modelPaginacao.TotalPaginas,
                    TotalRegistros = modelPaginacao.TotalRegistros,
                    Dados = PaginacaoTabelaResult<MenuDTO,MenuFiltro>.fromList<MenuModel,MenuDTO>(modelPaginacao.Dados, m => m)
                };
                return Response<PaginacaoTabelaResult<MenuDTO, MenuFiltro>>.Ok(paginacao);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<MenuDTO, MenuFiltro>>.Failed(ex.Message);
            }
        }

        public async Task<Response<List<MenuDTO>>> recuperaMenus(int idEmpresa)
        {
            try
            {
                var listaMenus = await _repository.recuperaMenus(idEmpresa);
                List<MenuDTO> dto = MenuDTO.FromList(listaMenus);
                dto = montaHieraquia(dto);

                return Response<List<MenuDTO>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<List<MenuDTO>>.Failed(ex.Message);
            }
        }

        public async Task<Response<List<MenuDTO>>> carregaComboMenu(int idEmpresa)
        {
            try
            {
                var listaMenus = await _repository.recuperaMenus(idEmpresa);
                List<MenuDTO> dto = MenuDTO.FromList(listaMenus);
                return Response<List<MenuDTO>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<List<MenuDTO>>.Failed(ex.Message);
            }
        }
        private List<MenuDTO> montaHieraquia(List<MenuDTO> menus)
        {
            var lookup = menus.ToDictionary(m => m.Id);

            foreach (var menu in menus)
            {
                if (menu.IdMenuPai.HasValue && lookup.TryGetValue(menu.IdMenuPai.Value, out var menuPai))
                {
                    menuPai.menuFilhos ??= new List<MenuDTO>();
                    menuPai.menuFilhos.Add(menu);
                }
            }
            return menus
         .Where(m => m.IdMenuPai == 0 || !m.IdMenuPai.HasValue)
         .OrderBy(m => m.DeMenu)
         .ToList();
        }

        public async Task<Response<List<MenuDTO>>> obterTodosMenus(MenuFiltro menu)
        {
            try
            {

                List<MenuDTO> listMenus = MenuDTO.FromList(await _repository.obterTodosMenus(menu));
                listMenus = montarPai(listMenus);

                if (listMenus.Count == 0)
                {
                    return Response<List<MenuDTO>>.Ok(listMenus, "Nenhum registro encontrado");
                }
                return Response<List<MenuDTO>>.Ok(listMenus);
            }
            catch (Exception ex)
            {
                return Response<List<MenuDTO>>.Failed(ex);
            }
        }
        private List<MenuDTO> montarPai(List<MenuDTO> menus)
        {
             var lookup = menus.ToDictionary(m => m.Id);

            foreach (var menu in menus)
            {
                if (menu.IdMenuPai.HasValue && lookup.TryGetValue(menu.IdMenuPai.Value, out var menuPai))
                {
                    menu.sMenuPai = menuPai.DeMenu;
                    menu.MenuPai = menuPai;
                }
            }
            return menus; 
        }
    }
}
