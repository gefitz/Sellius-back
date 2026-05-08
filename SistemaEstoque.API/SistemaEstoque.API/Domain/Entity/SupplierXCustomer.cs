using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;

namespace Sellius.API.Domain.Models
{
    public class SupplierXCustomer
    {
        public long CustomerId { get; init; }
        public long SupplierId { get; init; }
        
        public Customer.Customer? Customer { get; set; }
        public Supplier? Supplier { get; set; }
        public static implicit operator SupplierXCustomer(FornecedorXClienteDTO dto)
        {
            return new SupplierXCustomer
            {
                idCliente = dto.idCliente,
                SupplierId = dto.idFornecedor,
            };
        }
    }
}
