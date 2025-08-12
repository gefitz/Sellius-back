using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
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
                return await _context.Fornecedores.Where(f => f.id.Equals(obj.id)).FirstOrDefaultAsync();
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
                if (!string.IsNullOrEmpty( obj.Filtro.CNPJ))
                    query = query.Where(p => p.CNPJ.Equals(obj.Filtro.CNPJ));
                if (obj.Filtro.fAtivo != -1)
                    query = query.Where(p => p.fAtivo.Equals(obj.Filtro.fAtivo));
                query.Where(p => p.EmpresaId == obj.Filtro.EmpresaId);

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
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
    }
}
