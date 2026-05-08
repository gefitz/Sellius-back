using Sellius.API.DTOs;

namespace Sellius.API.Domain.Models
{
    public class State
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public List<City> Citys { get; set; }

        public static implicit operator State(EstadoDTO dto)
        {
            return new State { id = dto.id, Sigla = dto.Sigla, Estado = dto.Estado };
        }
    }
}