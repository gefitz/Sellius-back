namespace Sellius.API.Domain.Entity.EntityUsers
{
    public class Menu
    {
        public long Id { get; set; }
        public string DescMenu { get; set; }
        public string UrlMenu { get; set; }
        public string Icon { get; set; }
        public long? MenuFatherId { get; set; }
        public Guid? EnterpriseId { get; set; }
        public short Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }

        public List<TypeUserXMenu>? TypeUserXMenus { get; init; }
        
    }
}
