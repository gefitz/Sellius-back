using Sellius.API.Models.Produto;

namespace Sellius.API.DTOs.TabelasDTOs;

public class TabelaPrecoXProdutoDTO
{
    public string Produto { get; set; }
    public string Marca { get; set; }
    public decimal VlProduto { get; set; }
    public int IdTabelaPreco { get; set; }


    public static implicit operator TabelaPrecoXProdutoDTO(TabelaPrecoXProdutoModel model) => new()
    {
        Produto = model.Produto.Nome,
        VlProduto = model.vlPreco,
        IdTabelaPreco = model.idTabelaPreco
    };
}