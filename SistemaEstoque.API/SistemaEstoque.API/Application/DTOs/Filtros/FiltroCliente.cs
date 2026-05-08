namespace Sellius.API.DTOs.Filtros
{
    public class FiltroCliente
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public int CidadeId { get; set; }
        public short fAtivo { get; set; }
        public int EmpresaId { get; set; }
    }
}
