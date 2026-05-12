using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
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

        public async Task<Response<MenuRegister>> CriarMenu(MenuRegister register)
        {
            try
            {
                Menu menu = register;
                menu.DtCadastro = DateTime.UtcNow;
                menu.FAtivo = 1;

                if (await _repository.Create(menu))
                {
                    register = menu;
                    return Response<MenuRegister>.Ok(register);
                }
                return Response<MenuRegister>.Failed("Falha ao cadastrar o menu.");
            }
            catch (Exception ex)
            {
                return Response<MenuRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<MenuRegister>> UpdateMenu(MenuRegister register)
        {
            try
            {
                Menu model = register;
                model.DtAtualizacao = DateTime.UtcNow;

                if (await _repository.Update(model))
                {
                    return Response<MenuRegister>.Ok(model);
                }
                return Response<MenuRegister>.Failed("Falha ao tentar atualizar o menu.");
            }
            catch (Exception ex)
            {
                return Response<MenuRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<MenuRegister>> BuscaMenuPorId(int id)
        {
            try
            {
                Menu menu = new Menu { Id = id };
                menu = await _repository.BuscaDireto(menu);

                if (menu != null)
                    return Response<MenuRegister>.Ok(menu);

                return Response<MenuRegister>.Failed("Menu não localizado.");
            }
            catch (Exception ex)
            {
                return Response<MenuRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<MenuRegister>> InativarMenu(int id)
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
                return Response<MenuRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<PaginationTableResult<>>> ObterTodosMenus(PaginationTableResult<> paginacao)
        {
            try
            {
                PaginationTableResult<> modelPaginacao = new PaginationTableResult<>
                {
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    TotalRegistros = paginacao.TotalRegistros,
                };
                modelPaginacao = await _repository.Filtrar(modelPaginacao);
                //paginacao.Dados = MenuDTO.FromList(modelPaginacao.Dados);
                paginacao = new PaginationTableResult<>
                {
                    PaginaAtual = modelPaginacao.PaginaAtual,
                    TamanhoPagina = modelPaginacao.TamanhoPagina,
                    TotalPaginas = modelPaginacao.TotalPaginas,
                    TotalRegistros = modelPaginacao.TotalRegistros,
                    Dados = PaginationTableResult<>.fromList<Menu,MenuRegister>(modelPaginacao.Dados, m => m)
                };
                return Response<PaginationTableResult<>>.Ok(paginacao);
            }
            catch (Exception ex)
            {
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }
        }

        public async Task<Response<List<MenuRegister>>> recuperaMenus(int idEmpresa)
        {
            try
            {
                var listaMenus = await _repository.recuperaMenus(idEmpresa);
                List<MenuRegister> dto = MenuRegister.FromList(listaMenus);
                dto = montaHieraquia(dto);

                return Response<List<MenuRegister>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<List<MenuRegister>>.Failed(ex.Message);
            }
        }

        public async Task<Response<List<MenuRegister>>> carregaComboMenu(int idEmpresa)
        {
            try
            {
                var listaMenus = await _repository.recuperaMenus(idEmpresa);
                List<MenuRegister> dto = MenuRegister.FromList(listaMenus);
                return Response<List<MenuRegister>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<List<MenuRegister>>.Failed(ex.Message);
            }
        }
        private List<MenuRegister> montaHieraquia(List<MenuRegister> menus)
        {
            var lookup = menus.ToDictionary(m => m.Id);

            foreach (var menu in menus)
            {
                if (menu.IdMenuPai.HasValue && lookup.TryGetValue(menu.IdMenuPai.Value, out var menuPai))
                {
                    menuPai.menuFilhos ??= new List<MenuRegister>();
                    menuPai.menuFilhos.Add(menu);
                }
            }
            return menus
         .Where(m => m.IdMenuPai == 0 || !m.IdMenuPai.HasValue)
         .OrderBy(m => m.DeMenu)
         .ToList();
        }

        public async Task<Response<List<MenuRegister>>> obterTodosMenus(MenuFilter menu)
        {
            try
            {

                List<MenuRegister> listMenus = MenuRegister.FromList(await _repository.obterTodosMenus(menu));
                listMenus = montarPai(listMenus);

                if (listMenus.Count == 0)
                {
                    return Response<List<MenuRegister>>.Ok(listMenus, "Nenhum registro encontrado");
                }
                return Response<List<MenuRegister>>.Ok(listMenus);
            }
            catch (Exception ex)
            {
                return Response<List<MenuRegister>>.Failed(ex);
            }
        }
        private List<MenuRegister> montarPai(List<MenuRegister> menus)
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
