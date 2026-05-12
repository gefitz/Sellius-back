using Sellius.API.DTOs;

namespace Sellius.API.Domain.Entity
{
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public State State { get; set; }

    }
}
