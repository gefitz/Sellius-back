using Sellius.API.Enums;
using Sellius.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public int CidadeId { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        [DataType(DataType.Date)]
        public DateTime dthCadastro { get; set; } = DateTime.Now;
        public int EmpresaId { get; set; } = 0;
        public TipoUsuario TipoUsuario { get; set; }
        public short fAtivo { get; set; } = 0;

        public static implicit operator UsuarioDTO(UsuarioModel model)
        {
            return new UsuarioDTO
            {
                id = model.id,
                Nome = model.Nome,
                Documento = model.Documento,
                Email = model.Email,
                Rua = model.Rua,
                CidadeId = model.CidadeId,
                CEP = model.CEP,
                dthCadastro = model.dthCadastro,
                EmpresaId = model.EmpresaId,
                TipoUsuario = model.TipoUsuario
            };
        }
        public static List<UsuarioDTO> FromList(List<UsuarioModel> List)
        {
            List<UsuarioDTO> usuarioDTOs = new List<UsuarioDTO>();
            for (int i = 0; i < List.Count; i++)
            {
                usuarioDTOs.Add(usuarioDTOs[i]);
            }
            return usuarioDTOs;

        }
    }
}
