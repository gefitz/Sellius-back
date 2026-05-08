using Sellius.API.DTOs;
using Sellius.API.Models;

namespace Sellius.API.Domain.Models
{
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public State State { get; set; }

        public static implicit operator City(CidadeDTO cidade)
        {
            return new City
            {
                Cidade = cidade.Cidade,
                id = cidade.id,
                Estado = cidade.Estado
            };
        }

    }
}
