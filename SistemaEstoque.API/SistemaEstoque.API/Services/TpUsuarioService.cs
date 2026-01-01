using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Usuarios;
using Sellius.API.Repository.Usuarios.Interfaces;

namespace Sellius.API.Services
{
    public class TpUsuarioService
    {
        private TpUsuarioRepository _repository;

        public TpUsuarioService(TpUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<TpUsuarioDTO>> cadastrarTpUsuario(TpUsuarioDTO tpUsuario)
        {
            tpUsuario.dtCadastro = DateTime.UtcNow;
            if (await _repository.Create(tpUsuario))
            {
                if(tpUsuario.Menu != null)
                {
                    await adicionarTpUsuarioXMenu(tpUsuario);
                }

                if(tpUsuario.TpUsuarioConfiguracao != null)
                {
                    await adicionarTpUsuarioConfiguracao(tpUsuario);
                }
                return Response<TpUsuarioDTO>.Ok();
            }
            return Response<TpUsuarioDTO>.Failed("Erro ao tenta inserir o tipo de usuario");
        }
        public async Task<Response<TpUsuarioDTO>> Update(TpUsuarioDTO tpUsuario)
        {
            var ret = await buscarId(tpUsuario.id);

            if (ret != null && !ret.success)
            {
                return ret;
            }

            TpUsuarioModel model = ret.Data;
            tpUsuario.dtCadastro = model.dtCadastro;
            if (await _repository.Update(tpUsuario))
            {
                if(tpUsuario.Menu != null)
                {
                    await adicionarTpUsuarioXMenu(tpUsuario);
                }

                if(tpUsuario.TpUsuarioConfiguracao != null)
                {
                    await adicionarTpUsuarioConfiguracao(tpUsuario);
                }

                return Response<TpUsuarioDTO>.Ok();
            }
            return Response<TpUsuarioDTO>.Failed("Erro ao tenta inserir o tipo de usuario");
        }
        public async Task<Response<TpUsuarioDTO>> deletarTpUsuario(int idTpUsuario)
        {
            var ret = await buscarId(idTpUsuario);

            if (ret != null && !ret.success)
            {
                return ret;
            }

            TpUsuarioModel model = ret.Data;

            model.fAtivo = 0;

            return await Update(model);
        }
        public async Task<Response<PaginacaoTabelaResult<TpUsuarioDTO, TpUsuarioFiltro>>> paginacao(PaginacaoTabelaResult<TpUsuarioDTO, TpUsuarioFiltro> paginacao)
        {
            PaginacaoTabelaResult<TpUsuarioModel, TpUsuarioModel> modelPaginacao = new PaginacaoTabelaResult<TpUsuarioModel, TpUsuarioModel>
            {
                Filtro = paginacao.Filtro,
                PaginaAtual = paginacao.PaginaAtual,
                TamanhoPagina = paginacao.TamanhoPagina,
                TotalPaginas = paginacao.TotalPaginas,
                TotalRegistros = paginacao.TotalRegistros,
            };
            modelPaginacao = await _repository.Filtrar(modelPaginacao);
            //paginacao.Dados = MenuDTO.FromList(modelPaginacao.Dados);
            paginacao = new PaginacaoTabelaResult<TpUsuarioDTO, TpUsuarioFiltro>
            {
                PaginaAtual = modelPaginacao.PaginaAtual,
                TamanhoPagina = modelPaginacao.TamanhoPagina,
                TotalPaginas = modelPaginacao.TotalPaginas,
                TotalRegistros = modelPaginacao.TotalRegistros,
                Dados = TpUsuarioDTO.fromToList(modelPaginacao.Dados)
            };

            return Response<PaginacaoTabelaResult<TpUsuarioDTO, TpUsuarioFiltro>>.Ok(paginacao);
        }

        public async Task<Response<TpUsuarioDTO>> buscarId(int idTpUsuario)
        {
            TpUsuarioModel model = new TpUsuarioModel { id = idTpUsuario };
            TpUsuarioDTO dto = await _repository.BuscaDireto(model);
            {
                return Response<TpUsuarioDTO>.Ok(dto);
            }
            return Response<TpUsuarioDTO>.Failed("Erro ao tenta inserir o tipo de usuario");
        }

        public async Task<Response<List<TpUsuarioDTO>>> recuperaTpUsuarios(int idEmpresa)
        {
            List<TpUsuarioDTO> dto = TpUsuarioDTO.fromToList(await _repository.recuperaTpUsuarios(idEmpresa));
            {
                return Response<List<TpUsuarioDTO>>.Ok(dto);
            }
            return Response<List<TpUsuarioDTO>>.Failed("Erro ao tenta inserir o tipo de usuario");
        }
        private async Task adicionarTpUsuarioXMenu(TpUsuarioDTO tpUsuario)
        {
            try
            {
                if (tpUsuario != null && tpUsuario.Menu.Count != 0)
                {
                    List<TpUsuarioXMenu> usuarioXMenus = await _repository.obterTodosTpUsuarioXMenu(tpUsuario.id);

                    if (usuarioXMenus.Count != 0)
                    {
                        await _repository.RemoverTodosTpUsuariosXMenu(usuarioXMenus);
                    }
                    usuarioXMenus = new List<TpUsuarioXMenu>();
                    for (int i = 0; i < tpUsuario.Menu.Count; i++)
                    {
                        MenuDTO menuDTO = tpUsuario.Menu[i];
                        TpUsuarioXMenu tpUsuarioXMenu = new TpUsuarioXMenu();
                        tpUsuarioXMenu.idTpUsuario = tpUsuario.id;

                        //Se for um menu Filho coloca o menu pai na lista mesmo se nao for selecionado
                        if (menuDTO.MenuPai != null)
                        {
                            tpUsuarioXMenu.idMenu = menuDTO.MenuPai.Id;
                            var clone = tpUsuarioXMenu.Clone();

                            if (!usuarioXMenus.Any(x => x.idTpUsuario == clone.idTpUsuario
                                                     && x.idMenu == clone.idMenu))
                            {
                                usuarioXMenus.Add(clone);
                            }

                        }

                        tpUsuarioXMenu.idMenu = menuDTO.Id;
                        if (!usuarioXMenus.Any(x => x.idTpUsuario == tpUsuarioXMenu.idTpUsuario
                                                 && x.idMenu == tpUsuarioXMenu.idMenu))
                        {
                            usuarioXMenus.Add(tpUsuarioXMenu);
                        }

                    }
                    await _repository.AdicionarTodosTpUsuariosXMenu(usuarioXMenus);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task adicionarTpUsuarioConfiguracao(TpUsuarioDTO tpUsuarioDTO)
        {
            var config = await _repository.obterConfiguracao(tpUsuarioDTO.id);
            if (config != null)
            {
                await _repository.RemoverConfiguracao(config);
            }
            await _repository.AdicionarConfiguracao(tpUsuarioDTO.TpUsuarioConfiguracao);
        }
    }
}
