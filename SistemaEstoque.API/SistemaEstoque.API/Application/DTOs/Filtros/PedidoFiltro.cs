namespace Sellius.API.DTOs.Filtros
{
    public class PedidoFiltro
    {
        public DateTime dthPedido { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public short Finalizado { get; set; }
        public int EmpresaId { get; set; }
    }
}
