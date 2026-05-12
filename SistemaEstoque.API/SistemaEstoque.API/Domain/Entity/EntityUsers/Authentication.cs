using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Entity.EntityUsers
{
    public class Authentication
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        
        public Guid UserId { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public User? User { get; set; }
        public Enterprise? Enterprise { get; set; }

        public static implicit operator Authentication(LoginRegister register)
        {
            return new Authentication
            {
                Email = register.Email,
                //ClienteId = dto.ClienteId,
                //usuarioId = dto.usuarioId,
                //EmpresaId = dto.EmpresaId
            };
        }
    }
}
