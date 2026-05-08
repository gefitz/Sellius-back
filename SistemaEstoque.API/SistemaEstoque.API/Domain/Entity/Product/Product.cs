using Sellius.API.Domain.Models.Pedido;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;

namespace Sellius.API.Domain.Models.Product
{
    public class Product
    {
        public long Id { get; init; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public long? TypeProductId {  get; set; }
        public short Active { get; set; }
        public long SupplierId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public Supplier? Supplier { get; init; }
        public Enterprise? Enterprise { get; init; }

        public List<SaleOrdeXProduct> SaleOrders { get; set; }
        public TypeProduct? TypeProduct { get; init; }
        public List<PriceTableXProduct>? PriceTableXProducts { get; init; }


        #region Mapper
        public static implicit operator Product(FiltroProduto filtroProduto)
        {
            return new Product
            {
                Nome = filtroProduto.Nome,
                tipoProduto = new TypeProduct { id = filtroProduto.tipoProdutoId },

            };
        }
        public static implicit operator Product(ProdutoDTO produtoDTO)
        {
            return new Product
            {
                id = produtoDTO.id,
                Nome = produtoDTO.Nome,
                Descricao = produtoDTO.Descricao,
                TipoProdutoId = (int)produtoDTO.tipoProdutoId,
                valor = produtoDTO.valor,
                qtd = produtoDTO.qtd,
                dthCriacao = produtoDTO.dthCriacao,
                dthAlteracao = produtoDTO.dthAlteracao,
                FornecedorId = produtoDTO.FornecedorId,
                EmpresaId = (int)produtoDTO.EmpresaId,
                fAtivo = produtoDTO.fAtivo
            };
        }
        public static implicit operator Product(ProdutoTabela model)
        {
            return new Product
            {
                id = model.id
            };
        }
        #endregion
    }
}
