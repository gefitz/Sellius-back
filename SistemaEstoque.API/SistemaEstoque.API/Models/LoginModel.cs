using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Enums;
using Sellius.API.Models.Cliente;

namespace Sellius.API.Models
{
    public class LoginModel
    {
        public int id { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public bool fEmailConfirmado { get; set; } = false;
        public TipoUsuario TipoUsuario { get; set; } = 0;
        public int? usuarioId { get; set; } = 0;
        public UsuarioModel? Usuario { get; set; }
        public int EmpresaId { get; set; }
        public EmpresaModel Empresa { get; set; }

        public static implicit operator LoginModel(LoginDTO dto)
        {
            return new LoginModel
            {
                Email = dto.Email,
                //ClienteId = dto.ClienteId,
                //usuarioId = dto.usuarioId,
                //EmpresaId = dto.EmpresaId
            };
        }
    }
}
