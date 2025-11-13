namespace Sellius.API.DTOs.Filtros
{
    public class FiltroFornecedor
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public int CidadeId { get; set; }
        public int EstadoId { get; set; }
        public int EmpresaId { get; set; }
        public short fAtivo { get; set; }
    }
}
