using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using Sellius.API.Models;

namespace Sellius.API.DTOs.TabelasDTOs
{
    public class FornecedorXClienteDTO
    {
        public int idCliente { get; set; }
        public int idFornecedor { get; set; }
        public ClienteTabelaResult? Cliente { get; set; }
        public FornecedorTabelaResult? Fornecedor { get; set; }

        public static implicit operator FornecedorXClienteDTO(FornecedorXCliente model)
        {
            return new FornecedorXClienteDTO
            {
                idCliente = model.idCliente,
                idFornecedor = model.idFornecedor,
                Cliente = model.Cliente,
                Fornecedor = model.Fornecedor,
            };
        }
    }
}
