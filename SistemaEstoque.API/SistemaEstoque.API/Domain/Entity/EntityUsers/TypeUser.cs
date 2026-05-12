using Sellius.API.Domain.Entity.EntityEnterprises;

namespace Sellius.API.Domain.Entity.EntityUsers
{
    public class TypeUser
    {
        public long Id { get; init; }
        public string NameType { get; set; }
        public Guid EnterpriseId { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }

        public Enterprise? Enterprise { get; init; }

        public List<TypeUserXMenu>? TypeUserXMenus { get; init; }

        public UserConfiguration? TpUsuarioConfigurcao { get; init; }
        
    }

}
