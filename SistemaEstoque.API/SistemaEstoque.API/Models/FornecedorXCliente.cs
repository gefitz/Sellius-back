using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Cliente;

namespace Sellius.API.Models
{
    public class FornecedorXCliente
    {
        public int idCliente { get; set; }
        public int idFornecedor { get; set; }
        public ClienteModel Cliente { get; set; }
        public FornecedoresModel Fornecedor { get; set; }
        public static implicit operator FornecedorXCliente(FornecedorXClienteDTO dto)
        {
            return new FornecedorXCliente
            {
                idCliente = dto.idCliente,
                idFornecedor = dto.idFornecedor,
            };
        }
    }
}
