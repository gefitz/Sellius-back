using Sellius.API.Domain.Entity.EntityEnterprises;

namespace Sellius.API.Domain.Entity.EntityProduct
{
    public class TypeProduct
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public short Active { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public Enterprise? Enterprise { get; set; }
        public List<Product> Products { get; set; }
        
    }
}