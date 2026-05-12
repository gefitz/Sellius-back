using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class CadastroNovoEmpresaDTO
    {
        public EnterpriseRegister Empresa { get; set; }
        public LoginRegister Login { get; set; }
        public UserRegister Usuario { get; set; }
    }
}
