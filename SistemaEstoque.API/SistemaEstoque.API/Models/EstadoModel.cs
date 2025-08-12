using Sellius.API.DTOs;

namespace Sellius.API.Models
{
    public class EstadoModel
    {
        public int id { get; set; }
        public string Estado { get; set; }
        public string Sigla { get; set; }
        public List<CidadeModel> Cidade { get; set; }

        public static implicit operator EstadoModel(EstadoDTO dto)
        {
            return new EstadoModel { id = dto.id, Sigla = dto.Sigla, Estado = dto.Estado };
        }
    }
}