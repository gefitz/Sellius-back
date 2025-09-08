using Sellius.API.Context;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Repository.Produto.Interface;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Repository.Produto
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;
        public ProdutoRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<ProdutoModel> BuscaDireto(ProdutoModel obj)
        {

            try
            {

                var produto = await _context.Produtos.Include(tp => tp.tipoProduto).Include(f=>f.Fornecedor).Where(p => p.id == obj.id).AsNoTracking().FirstOrDefaultAsync();
                if(produto != null)
                {
                    return produto;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Create(ProdutoModel obj)
        {
            try
            {
                _context.Produtos.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
        }

        public async Task<bool> Delete(ProdutoModel obj)
        {
            try
            {
                _context.Produtos.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<PaginacaoTabelaResult<ProdutoModel,FiltroProduto>> Filtrar(PaginacaoTabelaResult<ProdutoModel,FiltroProduto> obj)
        {
            try
            {
                FiltroProduto filtro = (FiltroProduto)obj.Filtro;
                var query = _context.Produtos.AsQueryable();

                if(!string.IsNullOrEmpty(filtro.Nome))
                    query = query.Where(p => p.Nome.Contains(filtro.Nome));
                if(filtro.tipoProdutoId != 0)
                    query = query.Where(p => p.TipoProdutoId.Equals(filtro.tipoProdutoId));
                if (filtro.fAtivo != -1)
                    query = query.Where(p => p.fAtivo.Equals(filtro.fAtivo));
                if(filtro.FornecedorId != 0)
                    query = query.Where(p => p.FornecedorId.Equals(filtro.FornecedorId));
                query.Where(p => p.EmpresaId == 0);
                
                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados =   await query
                    .Include(tp => tp.tipoProduto)
                    .Include(f => f.Fornecedor)
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .ToListAsync();

                return obj;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

        public Task<IEnumerable<ProdutoModel>> Filtrar(ProdutoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ProdutoModel obj)
        {
            try
            {
                _context.Produtos.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }

        }
    }
}
