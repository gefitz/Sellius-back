using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Entity.EntityUsers
{
    public class TypeUserXMenu
    {
        public long TypeUserId { get; set; }
        public long MenuId { get; set; }
        public TypeUser? TypeUser { get; set; }
        public Menu? Menu { get; set; }
    }
}
