using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.Enterprise.Enterprise;
using Sellius.API.Domain.Enums;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Enums;
using Sellius.API.Infra.Context;
using Sellius.API.Repository.Empresa;
using Sellius.API.Repository.Empresa.Interface;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Services
{
    public class EmpresaService
    {
        private EmpresaRepository _repository;
        private UsuarioService _usarioService;
        private LoginService _loginService;
        private AppDbContext _context;
        private LicencaService _licenca;
        public EmpresaService(EmpresaRepository repository, UsuarioService suarioService, LoginService loginService, AppDbContext context, LicencaService licenca)
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

                    Enterprise emp = empresa.Empresa;
                    #region Criação da empresa
                    #region Gerar Licenca
                    //Gera a licenca da empresa e retorna o id da licenca
                    int idLicenca = await _licenca.GerarLicenca((TypeLicense)empresa.Empresa.TipoLicenca);

                    if (idLicenca == 0)
                        return Response<string>.Failed("Falha ao gerar a licenca da empresa");

                    emp.LicencaId = idLicenca;
                    emp.dthAlteracao = DateTime.UtcNow;
                    emp.dthCadastro = DateTime.UtcNow;
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
                    empresa.Usuario.tipoUsuario = 1;
                    Response<UserRegister> responseUsuario = await _usarioService.CriarUsuario(empresa.Usuario);
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
        public async Task<Response<EnterpriseRegister>> UpdateEmpresa(EnterpriseRegister empresa)
        {
            try
            {
                Enterprise emp = empresa;
                if (await _repository.Update(emp))
                {
                    return Response<EnterpriseRegister>.Ok(empresa);
                }
                return Response<EnterpriseRegister>.Failed("Erro ao fazer modeificação na empresa");
            }
            catch (Exception ex)
            {
                return Response<EnterpriseRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<EnterpriseRegister>> InativarEmpresa(int id)
        {
            try
            {

                var retBuscaEmpresa = await BuscaEmpresa(id);
                if (!retBuscaEmpresa.success)
                    return Response<EnterpriseRegister>.Failed(retBuscaEmpresa.message);
                retBuscaEmpresa.Data.fAtivo = 0;
                return await UpdateEmpresa(retBuscaEmpresa.Data);
            }
            catch (Exception ex)
            {
                return Response<EnterpriseRegister>.Failed(ex.Message);
            }
        }
        public async Task<Response<EnterpriseRegister>> BuscaEmpresa(int id)
        {
            try
            {
                Enterprise model = new Enterprise { id = id };

                model = await _repository.BuscaDireto(model);
                if (model == null)
                    return Response<EnterpriseRegister>.Failed("Empresa não locazilada");
                return Response<EnterpriseRegister>.Ok(model);
            }
            catch (Exception ex)
            {
                return Response<EnterpriseRegister>.Failed(ex.Message);
            }
        }
        public async Task<Response<PaginationTableResult<>>> obterTodasEmpresas(PaginationTableResult<> paginacao)
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
                paginacao.Dados = EnterpriseRegister.FromList(modelPaginacao.Dados);
                return Response<PaginationTableResult<>>.Ok(paginacao);

            }
            catch (Exception ex)
            {
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }
        }
        private async Task<bool> VereficaExistenciaEmpresa(EnterpriseRegister empresa)
        {
            Enterprise emp = await _repository.BuscaDireto(empresa);
            if (emp != null)
            {
                return true;
            }
            return false;
        }

    }
}
