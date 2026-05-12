using Sellius.API.Domain.Models;
using Sellius.API.DTOs;

namespace Sellius.API.Domain.Entity
{
    public class State
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public List<City> Citys { get; set; }
        
    }
}