using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Application.DTOs.RegisterDTOs
{
    public class TypeUserRegister
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid EnterpriseId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }
        public List<long> MenuId { get; set; }
        public UserConfiguration? UserConfiguration { get; set; }
    }
}
