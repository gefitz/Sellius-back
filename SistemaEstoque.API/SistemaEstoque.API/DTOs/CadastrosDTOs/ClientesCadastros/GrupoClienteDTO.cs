using Sellius.API.Models;
using Sellius.API.Models.Cliente;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros
{
    public class GrupoClienteDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int idEmpresa { get; set; }
        public DateTime dthCriacao { get; set; } = DateTime.Now;
        public DateTime dthAlteracao { get; set; } = DateTime.Now;
        public short fAtivo { get; set; }
        public static implicit operator GrupoClienteDTO(GrupoClienteModel dto)
        {
            return new GrupoClienteDTO
            {
                id = dto.id,
                nome = dto.nome,
                idEmpresa = dto.idEmpresa,
                fAtivo = dto.fAtivo,
                dthAlteracao = dto.dthAlteracao,
                dthCriacao = dto.dthCriacao,
            };
        }
        public static List<GrupoClienteDTO> FromToList(List<GrupoClienteModel> list)
        {
            List<GrupoClienteDTO> dto = new List<GrupoClienteDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(list[i]);
            }
            return dto;
        }

    }
}
