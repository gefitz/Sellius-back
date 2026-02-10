using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Enums;
using Sellius.API.Models.Empresa;

namespace Sellius.API.Models.Produto
{
    public class TabelaPrecoModel
    {
        public int Id { get; set; }
        public string descTabelaPreco { get; set; }
        public DateTime dtInicioVigencia { get; set; }
        public DateTime? dtFimVigencia { get; set; }
        public OrigemTabelaPreco idOrigemTabelaPreco { get; set; }
        public int idReferenciaOrigem { get; set; }
        public DateTime dtCadastro { get; set; }
        public DateTime? dtAtualizado { get; set; }
        public int idEmpresa { get; set; }
        public int idUsuario { get; set; }

        public EmpresaModel empresa { get; set; }

        public List<TabelaPrecoXProdutoModel> TabelaPrecoXProdutos { get; set; }

        public static implicit operator TabelaPrecoModel(TabelaPrecoDTO dto)
        {
            return new TabelaPrecoModel
            {
                Id = dto.Id,
                descTabelaPreco = dto.descTabelaPreco,
                dtInicioVigencia = dto.dtInicioVigencia,
                dtFimVigencia = dto.dtFimVigencia,
                idOrigemTabelaPreco = dto.idOrigemTabelaPreco,
                idReferenciaOrigem = dto.idReferenciaOrigem,
                dtCadastro = dto.dtCadastro == null ? DateTime.UtcNow : (DateTime)dto.dtCadastro,
                dtAtualizado = dto.dtAtualizado == null ? DateTime.UtcNow : (DateTime)dto.dtAtualizado,
                idUsuario = (int)dto.idUsuario,
                idEmpresa = (int)dto.idEmpresa, 
            };
        }
    }
}
