using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Pedidos.Interfaces;

namespace Sellius.API.Repository.Pedidos
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _logRepository;
        public PedidoRepository(AppDbContext context, LogRepository logRepository)
        {
            _context = context;
            _logRepository = logRepository;
        }

        public Task<PedidoModel> BuscaDireto(PedidoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(PedidoModel obj)
        {
            try
            {
                _context.Pedidos.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> Delete(PedidoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PedidoModel>> Filtrar(PedidoModel obj)
        {
            try
            {
                return await _context.Pedidos
                    //.Include(p => p.Produto).ThenInclude(tp => tp.TpProduto)
                    .Include(c => c.Cliente).ThenInclude(c => c.Cidade)
                    .Where(pe =>
                    //(string.IsNullOrEmpty(obj.Produto.Nome) || pe.Produto.Nome == obj.Produto.Nome)
                    //&&
                    string.IsNullOrEmpty(obj.Cliente.Nome) || pe.Cliente.Nome == obj.Cliente.Nome
                    ).ToListAsync();
            }catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PaginacaoTabelaResult<PedidoModel, PedidoFiltro>> Filtrar(PaginacaoTabelaResult<PedidoModel, PedidoFiltro> obj)
        {
            try
            {

                var query = _context.Pedidos.AsQueryable();

                if (obj.Filtro.ClienteId != 0)
                    query = query.Where(p => p.ClienteId.Equals(obj.Filtro.ClienteId));
                if (obj.Filtro.ClienteId != 0) 
                    query = query.Where(p => p.UsuarioId.Equals(obj.Filtro.UsuarioId));
                if (obj.Filtro.Finalizado != 0)
                    query = query.Where(p => p.ClienteId.Equals(obj.Filtro.Finalizado));

                //query = query.Where(p => p.EmpresaId.Equals(obj.Filtro.EmpresaId));
                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .Include(p => p.Cliente)
                    .ThenInclude(c => c.Cidade)
                    .ThenInclude(ci => ci.Estado)
                    .Include (p => p.Produto)
                    .ThenInclude(pxp => pxp.Produto)
                    .ThenInclude(tp => tp.tipoProduto)
                    .Include(p =>  p.Produto)
                    .ThenInclude(pxp => pxp.Produto)
                    .ThenInclude(f =>  f.Fornecedor)
                    .Include (p => p.Usuario)
                    .ToListAsync();

                return obj;
            }
            catch (Exception ex)
            {
                _logRepository.Error(ex);
                throw;
            }
        }

        public Task<bool> Update(PedidoModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
