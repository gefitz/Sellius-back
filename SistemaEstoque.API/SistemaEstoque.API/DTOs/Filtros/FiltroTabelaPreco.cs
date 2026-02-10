using Sellius.API.Enums;

namespace Sellius.API.DTOs.Filtros
{
    public class FiltroTabelaPreco
    {
        public string descTabelaPreco { get; set; }
        public int idOrigemtabelaPreco { get; set; }

        public DateTime dtInicialPesquisaInicioVigencia { get; set; }
        public DateTime dtFimPesquisaInicioVigencia { get; set; }
        public int? idEmpresa { get; set; }
    }
}