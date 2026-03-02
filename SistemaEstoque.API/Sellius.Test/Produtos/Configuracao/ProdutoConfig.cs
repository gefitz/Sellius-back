using Moq;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Repository.Produto;
using Sellius.API.Repository.Produto.Interface;
using Sellius.API.Services.Produtos;

namespace Sellius.Test.Produtos.Configuracao;

public abstract class ProdutoConfig
{
    protected readonly Mock<IProdutoRepository> produtoRepository;
    protected readonly ProdutoService produtoService;
    protected ProdutoConfig()
    {
        produtoRepository = new Mock<IProdutoRepository>();
        produtoService = new ProdutoService(produtoRepository.Object);
    }

}