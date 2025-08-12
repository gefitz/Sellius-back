using Sellius.API.Models;

namespace Sellius.API.DTOs
{
    public class EstadoDTO
    {
        public int id { get; set; }
        public string Estado { get; set; }
        public string Sigla { get; set; }

        public static implicit operator EstadoDTO(EstadoModel dto)
        {
            return new EstadoDTO { id = dto.id, Sigla = dto.Sigla, Estado = dto.Estado };
        }
        public static List<EstadoDTO> toList(IEnumerable<EstadoModel> estados)
        {
            List<EstadoDTO> estadoDTOs = new List<EstadoDTO>();
            foreach(var estado in estados)
            {
                estadoDTOs.Add(estado);
            }
            return estadoDTOs;
        }
    }
}
