using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Infra.Repository.Users;

namespace Sellius.API.Services
{
    public class TpUsuarioService
    {
        private TpUserRepository _repository;

        public TpUsuarioService(TpUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<TypeUserRegister>> cadastrarTpUsuario(TypeUserRegister tpUsuario)
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
                return Response<TypeUserRegister>.Ok();
            }
            return Response<TypeUserRegister>.Failed("Erro ao tenta inserir o tipo de usuario");
        }
        public async Task<Response<TypeUserRegister>> Update(TypeUserRegister tpUsuario)
        {
            var ret = await buscarId(tpUsuario.id);

            if (ret != null && !ret.success)
            {
                return ret;
            }

            TypeUser model = ret.Data;
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

                return Response<TypeUserRegister>.Ok();
            }
            return Response<TypeUserRegister>.Failed("Erro ao tenta inserir o tipo de usuario");
        }
        public async Task<Response<TypeUserRegister>> deletarTpUsuario(int idTpUsuario)
        {
            var ret = await buscarId(idTpUsuario);

            if (ret != null && !ret.success)
            {
                return ret;
            }

            TypeUser model = ret.Data;

            model.fAtivo = 0;

            return await Update(model);
        }
        public async Task<Response<PaginationTableResult<>>> paginacao(PaginationTableResult<> paginacao)
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
                Dados = TypeUserRegister.fromToList(modelPaginacao.Dados)
            };

            return Response<PaginationTableResult<>>.Ok(paginacao);
        }

        public async Task<Response<TypeUserRegister>> buscarId(int idTpUsuario)
        {
            TypeUser model = new TypeUser { id = idTpUsuario };
            TypeUserRegister dto = await _repository.BuscaDireto(model);
            {
                return Response<TypeUserRegister>.Ok(dto);
            }
            return Response<TypeUserRegister>.Failed("Erro ao tenta inserir o tipo de usuario");
        }

        public async Task<Response<List<TypeUserRegister>>> recuperaTpUsuarios(int idEmpresa)
        {
            List<TypeUserRegister> dto = TypeUserRegister.fromToList(await _repository.recuperaTpUsuarios(idEmpresa));
            {
                return Response<List<TypeUserRegister>>.Ok(dto);
            }
            return Response<List<TypeUserRegister>>.Failed("Erro ao tenta inserir o tipo de usuario");
        }
        private async Task adicionarTpUsuarioXMenu(TypeUserRegister tpUsuario)
        {
            try
            {
                if (tpUsuario != null && tpUsuario.Menu.Count != 0)
                {
                    List<TypeUserXMenu> usuarioXMenus = await _repository.obterTodosTpUsuarioXMenu(tpUsuario.id);

                    if (usuarioXMenus.Count != 0)
                    {
                        await _repository.RemoverTodosTpUsuariosXMenu(usuarioXMenus);
                    }
                    usuarioXMenus = new List<TypeUserXMenu>();
                    for (int i = 0; i < tpUsuario.Menu.Count; i++)
                    {
                        MenuRegister menuRegister = tpUsuario.Menu[i];
                        TypeUserXMenu typeUserXMenu = new TypeUserXMenu();
                        typeUserXMenu.idTpUsuario = tpUsuario.id;

                        //Se for um menu Filho coloca o menu pai na lista mesmo se nao for selecionado
                        if (menuRegister.MenuPai != null)
                        {
                            typeUserXMenu.idMenu = menuRegister.MenuPai.Id;
                            var clone = typeUserXMenu.Clone();

                            if (!usuarioXMenus.Any(x => x.idTpUsuario == clone.idTpUsuario
                                                     && x.idMenu == clone.idMenu))
                            {
                                usuarioXMenus.Add(clone);
                            }

                        }

                        typeUserXMenu.idMenu = menuRegister.Id;
                        if (!usuarioXMenus.Any(x => x.idTpUsuario == typeUserXMenu.idTpUsuario
                                                 && x.idMenu == typeUserXMenu.idMenu))
                        {
                            usuarioXMenus.Add(typeUserXMenu);
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
        private async Task adicionarTpUsuarioConfiguracao(TypeUserRegister typeUserRegister)
        {
            var config = await _repository.obterConfiguracao(typeUserRegister.id);
            if (config != null)
            {
                await _repository.RemoverConfiguracao(config);
            }
            await _repository.AdicionarConfiguracao(typeUserRegister.TpUsuarioConfiguracao);
        }

        public async Task<Response<List<TypeUserRegister>>> obterTodosTpUsuarios(int idEmpresa)
        {
            return Response<List<TypeUserRegister>>.Ok(TypeUserRegister.fromToList(await _repository.obterTodosTpUsuarios(idEmpresa)));
        }
    }
}
