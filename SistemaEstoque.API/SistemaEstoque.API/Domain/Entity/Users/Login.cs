using Sellius.API.Domain.Models.Enterprise;
using Sellius.API.Domain.Models.Users;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Enums;

namespace Sellius.API.Models.Usuario
{
    public class Login
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        
        public Guid UserId { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public User? User { get; set; }
        public Enterprise? Enterprise { get; set; }

        public static implicit operator Login(LoginDTO dto)
        {
            return new Login
            {
                Email = dto.Email,
                //ClienteId = dto.ClienteId,
                //usuarioId = dto.usuarioId,
                //EmpresaId = dto.EmpresaId
            };
        }
    }
}
