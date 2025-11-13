using Sellius.API.Context;
using Sellius.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sellius.API.Repository.Cliente.Interfaces;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Models.Cliente;

namespace Sellius.API.Repository.Cliente
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;
        public ClienteRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<ClienteModel> BuscaDireto(ClienteModel obj)
        {
            try
            {
                return await _context.Clientes
                    .Include(c => c.Cidade)
                    .ThenInclude(e => e.Estado)
                    .Include(p => p.Pedidos)
                    .ThenInclude(p => p.Produto)
                    .Include(s =>  s.Grupo)
                    .Include( s => s.segmentacao)
                    .Where(c => c.id == obj.id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Create(ClienteModel obj)
        {
            try
            {
                _context.Clientes.Add(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Falha ao cadastrar o cliente " + obj.Nome + ".\n erro: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(ClienteModel obj)
        {
            try
            {
                _context.Clientes.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<PaginacaoTabelaResult<ClienteModel,FiltroCliente>> Filtrar(PaginacaoTabelaResult<ClienteModel, FiltroCliente> obj)
        {
            try
            {

                var query = _context.Clientes.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.Nome))
                    query = query.Where(p => p.Nome.Contains(obj.Filtro.Nome));
                if (obj.Filtro.CidadeId != 0)
                    query = query.Where(p => p.CidadeId.Equals(obj.Filtro.CidadeId));
                if (obj.Filtro.fAtivo != -1)
                    query = query.Where(p => p.fAtivo.Equals(obj.Filtro.fAtivo));
                if (!string.IsNullOrEmpty(obj.Filtro.Documento))
                    query = query.Where(p => p.Documento.Equals(obj.Filtro.Documento));
                query.Where(p => p.EmpresaId == obj.Filtro.EmpresaId);

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .Include(c => c.Cidade)
                    .ThenInclude(e => e.Estado)
                    .Include(p => p.Pedidos)
                    .ThenInclude(p => p.Produto)
                    .Include(s => s.Grupo)
                    .Include(s => s.segmentacao)
                    .ToListAsync();

                return obj;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

        public Task<IEnumerable<ClienteModel>> Filtrar(ClienteModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ClienteModel obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
