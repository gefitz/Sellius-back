using Sellius.API.Models.Cliente;

namespace Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros
{
    public class SegmentacaoDTO
    {
        public int id { get; set; }
        public string Segmento { get; set; }
        public int idEmpresa { get; set; }
        public short fAtivo { get; set; }
        public DateTime dthCriacao { get; set; } = DateTime.Now;
        public DateTime? dthAlteracao { get; set; } = DateTime.Now;

        public static implicit operator SegmentacaoDTO(SegmentacaoModel model)
        {
            return new SegmentacaoDTO
            {
                id = model.id,
                Segmento = model.Segmento,
                idEmpresa =  model.idEmpresa,
                fAtivo = model.fAtivo,
                dthCriacao = model.dthCriacao,
                dthAlteracao = model.dthAlteracao
            };
        }
        public static List<SegmentacaoDTO> FromToList(List<SegmentacaoModel> models)
        {
            List<SegmentacaoDTO> dtos = new List<SegmentacaoDTO>();
            for (int i = 0; i < models.Count; i++)
            {
                dtos.Add(models[i]);
            }
            return dtos;
        }
    }
}
