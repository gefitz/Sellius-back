using Moq;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Models.Produto;
using Sellius.Test.Produtos.Configuracao;
using Xunit;

namespace Sellius.Test.Produtos.Services;

public sealed class ProdutoInsercao : ProdutoConfig
{
    [Fact]
    [Trait("Insercao", "Services")]
    public async void Insercao()
    {
        
        ProdutoDTO produtoDTO = new ProdutoDTO()
        {
            id = 0,
            Nome = "Feijao de carioca 1kg",
            Descricao = "Feijao de carioca 1kg",
            tipoProdutoId = 0,
            EmpresaId = 1,
            fAtivo = 1,
            dthCriacao = DateTime.UtcNow,
            dthAlteracao =  DateTime.UtcNow,
            valor = (decimal)1.20,
            qtd = 15,
            FornecedorId = 0
            
        };
        // Arrange
        produtoRepository.Setup(r => r.Create(It.IsAny<ProdutoModel>())).ReturnsAsync(true);
        
        //Act
        var result = await produtoService.CadastrarProduto(produtoDTO);
        
        //Asserts
        Assert.True(result.success);
        
        produtoRepository.Verify(r=> r.Create(It.IsAny<ProdutoModel>()),Times.Once);
        
    }
}