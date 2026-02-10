using Sellius.API.Enums;
using Sellius.API.Models.Empresa;
using Sellius.API.Models.Produto;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class TabelaPrecoDTO
    {
        public int Id { get; set; }
        public string descTabelaPreco { get; set; }
        public DateTime dtInicioVigencia { get; set; }
        public DateTime? dtFimVigencia { get; set; }
        public OrigemTabelaPreco idOrigemTabelaPreco { get; set; }
        public int idReferenciaOrigem { get; set; }
        public DateTime? dtCadastro { get; set; }
        public DateTime? dtAtualizado { get; set; }
        public int? idEmpresa { get; set; }
        public int? idUsuario { get; set; }

        public static implicit operator TabelaPrecoDTO( TabelaPrecoModel dto)
        {
            return new TabelaPrecoDTO
            {
                Id = dto.Id,
                descTabelaPreco = dto.descTabelaPreco,
                dtInicioVigencia = dto.dtInicioVigencia,
                dtFimVigencia = dto.dtFimVigencia,
                idOrigemTabelaPreco = dto.idOrigemTabelaPreco,
                idReferenciaOrigem = dto.idReferenciaOrigem,
                dtCadastro = dto.dtCadastro,
                dtAtualizado = dto.dtAtualizado,
                idUsuario = dto.idUsuario,
                idEmpresa = dto.idEmpresa,
            };
        }

        public static List<TabelaPrecoDTO> FromList(List<TabelaPrecoModel> dto)
        {
            List<TabelaPrecoDTO>dtos = new List<TabelaPrecoDTO>();

            foreach (var tabela in dto)
            {
                dtos.Add(tabela);    
            }

            return dtos;
        }

    }
}
