using Sellius.API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.Models
{
    public class CidadeModel
    {
        public int id { get; set; }
        public string Cidade { get; set; }
        public EstadoModel Estado { get; set; }
        public int EstadoId { get; set; }

        public static implicit operator CidadeModel(CidadeDTO cidade)
        {
            return new CidadeModel
            {
                Cidade = cidade.Cidade,
                id = cidade.id,
                Estado = cidade.Estado
            };
        }

    }
}
