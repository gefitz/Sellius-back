using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Models.Product
{
    public class PriceTable
    {
        public long Id { get; set; }
        public string DescPriceTable { get; set; }
        public DateTime InitialValidateDate { get; set; }
        public DateTime? FinalValidateDate { get; set; }
        public long SupplierId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
        public Guid EnterpriseId { get; set; }
        public Guid UserId { get; set; }

        public Enterprise? Enterprise { get; set; }
        public Supplier? Supplier { get; set; }
        public List<PriceTableXProduct> TabelaPrecoXProdutos { get; set; }

        public static implicit operator PriceTable(TabelaPrecoDTO dto)
        {
            return new PriceTable
            {
                Id = dto.Id,
                descTabelaPreco = dto.descTabelaPreco,
                dtInicioVigencia = dto.dtInicioVigencia,
                dtFimVigencia = dto.dtFimVigencia,
                idOrigemTabelaPreco = dto.idOrigemTabelaPreco,
                idReferenciaOrigem = dto.idReferenciaOrigem,
                dtCadastro = dto.dtCadastro == null ? DateTime.UtcNow : (DateTime)dto.dtCadastro,
                dtAtualizado = dto.dtAtualizado == null ? DateTime.UtcNow : (DateTime)dto.dtAtualizado,
                idUsuario = (int)dto.idUsuario,
                idEmpresa = (int)dto.idEmpresa, 
            };
        }
    }
}
