using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sellius.API.Models.Cliente
{
    [Table("Segmentacaoes")]
    public class SegmentacaoModel
    {
        public int id { get; set; }
        public string Segmento { get; set; }
        public int idEmpresa { get; set; }
        public DateTime dthCriacao { get; set; }
        public DateTime dthAlteracao { get; set; }
        public short fAtivo { get; set; }
        public EmpresaModel Empresa { get; set; }

        public static implicit operator SegmentacaoModel(SegmentacaoDTO dTO)
        {
            return new SegmentacaoModel
            {
                id = dTO.id,
                Segmento = dTO.Segmento,
                idEmpresa = dTO.idEmpresa,
                dthAlteracao = (DateTime)dTO.dthAlteracao,
                dthCriacao = dTO.dthCriacao,
                fAtivo = dTO.fAtivo,

            };
        }
    }
}
