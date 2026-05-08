using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Models.Product
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


        public static implicit operator TypeProduct(TipoProdutoDTO dto)
        {
            return new TypeProduct
            {
                Tipo = dto.Tipo,
                Descricao = dto.Descricao,
                Empresaid = dto.EmpresaId,
                fAtivo = dto.fAtivo,
                id = dto.id
            };
        }
    }
}