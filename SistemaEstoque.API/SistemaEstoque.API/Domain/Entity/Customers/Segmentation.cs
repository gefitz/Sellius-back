using System.ComponentModel.DataAnnotations.Schema;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;

namespace Sellius.API.Domain.Models.Customer
{
    public class Segmentation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public Entity.Enterprise.Enterprise Enterprise { get; set; }

        public static implicit operator Segmentation(SegmentacaoDTO dTO)
        {
            return new Segmentation
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
