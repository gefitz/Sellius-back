using Sellius.API.Context;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Enums;
using Sellius.API.Models;
using Sellius.API.Repository.Empresa.Interface;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Services
{
    public class EmpresaService
    {
        private IEmpresaRepository _repository;
        private UsuarioService _usarioService;
        private LoginService _loginService;
        private AppDbContext _context;
        private LicencaService _licenca;
        public EmpresaService(IEmpresaRepository repository, UsuarioService suarioService, LoginService loginService, AppDbContext context, LicencaService licenca)
        {
            _repository = repository;
            _usarioService = suarioService;
            _loginService = loginService;
            _context = context;
            _licenca = licenca;
        }

        public async Task<Response<string>> CadastrarNovaEmpresa(CadastroNovoEmpresaDTO empresa)
        {

            Response<string> token = new Response<string>();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await _usarioService.VereficaExistenciaUsuario(empresa.Usuario))

                        return Response<string>.Failed("Esse email de usuario ja está sendo utilizado");

                    EmpresaModel emp = empresa.Empresa;
                    #region Criação da empresa
                    #region Gerar Licenca
                    //Gera a licenca da empresa e retorna o id da licenca
                    int idLicenca = await _licenca.GerarLicenca((TipoLicenca)empresa.Empresa.TipoLicenca);

                    if (idLicenca == 0)
                        return Response<string>.Failed("Falha ao gerar a licenca da empresa");

                    emp.LicencaId = idLicenca;
                    emp.dthAlteracao = DateTime.Now;
                    emp.dthCadastro = DateTime.Now;
                    emp.fAtivo = 1;
                    #endregion
                    if (await VereficaExistenciaEmpresa(empresa.Empresa))
                        return Response<string>.Failed("Empresa com esse cnpj ja está cadastrada");

                    if (!await _repository.Create(emp))

                        return Response<string>.Failed("Falha ao cadastrar a empresa");
                    #endregion

                    #region Criacao do usuario admin

                    empresa.Empresa = emp;
                    empresa.Usuario.EmpresaId = empresa.Empresa.id;
                    empresa.Usuario.TipoUsuario = Enums.TipoUsuario.Adm;
                    Response<UsuarioDTO> responseUsuario = await _usarioService.CriarUsuario(empresa.Usuario);
                    if (responseUsuario == null || !responseUsuario.success)
                        return Response<string>.Failed(responseUsuario.errorMessage);

                    #region Criacao do login do admin
                    
                    token = await _loginService.CriarLogin(empresa.Login, responseUsuario.Data);
                    if (token == null || !token.success)

                        return Response<string>.Failed(token.errorMessage);
                    #endregion

                    #endregion

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Response<string>.Failed(ex.Message);
                }
            }
            return token;


        }
        public async Task<Response<EmpresaDTO>> UpdateEmpresa(EmpresaDTO empresa)
        {
            try
            {
                EmpresaModel emp = empresa;
                if (await _repository.Update(emp))
                {
                    return Response<EmpresaDTO>.Ok(empresa);
                }
                return Response<EmpresaDTO>.Failed("Erro ao fazer modeificação na empresa");
            }
            catch (Exception ex)
            {
                return Response<EmpresaDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<EmpresaDTO>> InativarEmpresa(int id)
        {
            try
            {

                var retBuscaEmpresa = await BuscaEmpresa(id);
                if (!retBuscaEmpresa.success)
                    return Response<EmpresaDTO>.Failed(retBuscaEmpresa.message);
                retBuscaEmpresa.Data.fAtivo = 0;
                return await UpdateEmpresa(retBuscaEmpresa.Data);
            }
            catch (Exception ex)
            {
                return Response<EmpresaDTO>.Failed(ex.Message);
            }
        }
        public async Task<Response<EmpresaDTO>> BuscaEmpresa(int id)
        {
            try
            {
                EmpresaModel model = new EmpresaModel { id = id };

                model = await _repository.BuscaDireto(model);
                if (model == null)
                    return Response<EmpresaDTO>.Failed("Empresa não locazilada");
                return Response<EmpresaDTO>.Ok(model);
            }
            catch (Exception ex)
            {
                return Response<EmpresaDTO>.Failed(ex.Message);
            }
        }
        public async Task<Response<PaginacaoTabelaResult<EmpresaDTO, EmpresaDTO>>> obterTodasEmpresas(PaginacaoTabelaResult<EmpresaDTO, EmpresaDTO> paginacao)
        {
            try
            {
                PaginacaoTabelaResult<EmpresaModel, EmpresaModel> modelPaginacao = new PaginacaoTabelaResult<EmpresaModel, EmpresaModel>
                {
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    TotalRegistros = paginacao.TotalRegistros,
                };
                modelPaginacao = await _repository.Filtrar(modelPaginacao);
                paginacao.Dados = EmpresaDTO.FromList(modelPaginacao.Dados);
                return Response<PaginacaoTabelaResult<EmpresaDTO, EmpresaDTO>>.Ok(paginacao);

            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<EmpresaDTO, EmpresaDTO>>.Failed(ex.Message);
            }
        }
        private async Task<bool> VereficaExistenciaEmpresa(EmpresaDTO empresa)
        {
            EmpresaModel emp = await _repository.BuscaDireto(empresa);
            if (emp != null)
            {
                return true;
            }
            return false;
        }

    }
}
