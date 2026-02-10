using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Fornecedor.Interfaces;

namespace Sellius.API.Repository.Fornecedor
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _logRepository;
        public FornecedorRepository(AppDbContext context, LogRepository logRepository)
        {
            _context = context;
            _logRepository = logRepository;
        }

        public async Task<FornecedoresModel> BuscaDireto(FornecedoresModel obj)
        {
            try
            {
                return await _context.Fornecedores.Where(f => f.id.Equals(obj.id)).Include(c => c.Cidade).ThenInclude(e => e.Estado).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logRepository.Error(ex);
                return null;
            }
        }

        public async Task<bool> Create(FornecedoresModel obj)
        {
            try
            {
                _context.Fornecedores.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logRepository.Error(ex);
                return false;
            }

        }

        public Task<bool> Delete(FornecedoresModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FornecedoresModel>> Filtrar(FornecedoresModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<FornecedoresModel, FornecedoresModel>> Filtrar(PaginacaoTabelaResult<FornecedoresModel, FornecedoresModel> obj)
        {
            try
            {

                var query = _context.Fornecedores.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.Nome))
                    query = query.Where(p => p.Nome.Contains(obj.Filtro.Nome));

                if (!string.IsNullOrEmpty(obj.Filtro.CNPJ))
                    query = query.Where(p => p.CNPJ == obj.Filtro.CNPJ);

                if (obj.Filtro.fAtivo != -1)
                    query = query.Where(p => p.fAtivo == obj.Filtro.fAtivo);

                if (obj.Filtro.CidadeId != -1)
                    query = query.Where(p => p.CidadeId == obj.Filtro.CidadeId);

                if (obj.Filtro.Cidade != null && obj.Filtro.Cidade.EstadoId != -1)
                    query = query.Where(p => p.Cidade != null && p.Cidade.EstadoId == obj.Filtro.Cidade.EstadoId);

                query = query.Where(p => p.EmpresaId == obj.Filtro.EmpresaId);

                obj.TotalRegistros = await query.CountAsync();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip(obj.PaginaAtual * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .Include(c => c.Cidade)
                        .ThenInclude(c => c.Estado)
                    .ToListAsync();

                return obj;
            }
            catch (Exception ex)
            {
                _logRepository.Error(ex);
                return null;
            }
        }

        public async Task<bool> Update(FornecedoresModel obj)
        {
            try
            {
                _context.Fornecedores.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logRepository.Error(ex);
                return false;
            }
        }
        public Task<List<FornecedoresModel>> CarregarComboFornecedor(FornecedoresModel model)
        {
            return _context.Fornecedores.Where(f => f.EmpresaId == model.EmpresaId).ToListAsync();
        }

        public bool addFornecedorXCliente(FornecedorXCliente model)
        {
            try
            {
                _context.FornecedorXClientes.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logRepository.Error(ex);
                return false;
            }
        }
        public async Task<PaginacaoTabelaResult<FornecedorXClienteDTO, FornecedorXClienteFiltro>> obterFornecedorXClientePaginada(PaginacaoTabelaResult<FornecedorXClienteDTO, FornecedorXClienteFiltro> obj)
        {
            try
            {

                var query = _context.FornecedorXClientes.AsQueryable();

                //Filtro que vai ser utilzado na tela do fornecedor
                if(obj.Filtro.idFornecedor != 0)
                {
                    query.Where(f => f.idFornecedor == obj.Filtro.idFornecedor);
                }

                //Filtro que vai ser utilizado na tela do cliente
                if(obj.Filtro.idCliente != 0)
                {
                    query.Where(f => f.idCliente == obj.Filtro.idCliente);
                }
                

                obj.TotalRegistros = await query.CountAsync();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                List<FornecedorXCliente> dados =  await query
                    .Skip(obj.PaginaAtual * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .Include(c => c.Cliente)
                    .ThenInclude(ci=> ci.Cidade)
                    .ThenInclude(e => e.Estado)
                    .Include(f => f.Fornecedor)
                    .ToListAsync();

                obj.Dados = PaginacaoTabelaResult<FornecedorXClienteDTO,FornecedorXCliente>.fromList<FornecedorXCliente,FornecedorXClienteDTO>(dados,fxc => fxc);

                return obj;
            }
            catch (Exception ex)
            {
                _logRepository.Error(ex);
                return null;
            }
        }
        public async Task<bool> removerVinculoFornecedorXCliente(FornecedorXCliente fornecedorXCliente)
        {
            try
            {
                _context.FornecedorXClientes.Remove(fornecedorXCliente);
                _context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                _logRepository.Error(ex);
                return false;
            }
        }
    }
}
