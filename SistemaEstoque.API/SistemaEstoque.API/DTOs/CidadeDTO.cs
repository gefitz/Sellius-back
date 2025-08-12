using Sellius.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs
{
    public class CidadeDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Cidade e obrigatorio")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Estado e obrigatorio")]
        public EstadoDTO Estado { get; set; }

        public static implicit operator CidadeDTO(CidadeModel cidade)
        {
            return new CidadeDTO
            {
                Cidade = cidade.Cidade,
                id = cidade.id,
                Estado = cidade.Estado
            };
        }
        public static List<CidadeDTO> toList(IEnumerable<CidadeModel> cidades)
        {
            List<CidadeDTO> cidadeDTOs = new List<CidadeDTO>();
            foreach (var cidade in cidades)
            {
                cidadeDTOs.Add(cidade);
            }
            return cidadeDTOs;
        }

    }
}
