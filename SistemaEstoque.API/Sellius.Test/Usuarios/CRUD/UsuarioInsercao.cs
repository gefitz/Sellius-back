using Moq;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Models.Usuario;
using Sellius.Test.Usuarios.Configuracao;
using Xunit;

namespace Sellius.Test.Usuarios.Insercao;

public class UsuarioInsercao : UsuarioConfiguracao
{
    [Fact]
    [Trait("Insercao", "Services")]
    public async Task Insercao()
    {
        UsuarioDTO dto = new UsuarioDTO()
        {
            id = 0,
            Nome = "Geovane de Almeida Fitz",
            Documento = "09563364937",
            Email = "gege.fitz@gmail.com",
            CidadeId = 1,
            CEP = "83409370",
            Rua = "Julio Ribeiro 392",
            dthCadastro = DateTime.UtcNow,
            EmpresaId = 1,
            tipoUsuario = 1,
            fAtivo = 1
        };
        //Arrange
        usuarioRepository.Setup(u => u.Create(It.IsAny<UsuarioModel>())).ReturnsAsync(true);
        
        //Act
        var result = await usuarioService.CriarUsuario(dto);
        
        //Asserts
        Assert.True(result.success);
        
        usuarioRepository.Verify(r=> r.Create(It.IsAny<UsuarioModel>()), Times.Once);

    }
    [Fact]
    [Trait("ExisteUsuario", "Services")]
    public async Task ExisteUsuario()
    {
        UsuarioDTO dto = new UsuarioDTO()
        {
            id = 0,
            Nome = "Geovane de Almeida Fitz",
            Documento = "09563364937",
            Email = "almeidafitz@gmail.com",
            CidadeId = 1,
            CEP = "83409370",
            Rua = "Julio Ribeiro 392",
            dthCadastro = DateTime.UtcNow,
            EmpresaId = 1,
            tipoUsuario = 1,
            fAtivo = 1
        };
        //Arrange
        usuarioRepository.Setup(u => u.BuscaDiretoEmail(It.IsAny<UsuarioModel>())).ReturnsAsync(new UsuarioModel());
        
        //Act
        var result = await usuarioService.CriarUsuario(dto);
        
        //Asserts
        Assert.False(result.success);
        
        usuarioRepository.Verify(r=> r.BuscaDiretoEmail(It.IsAny<UsuarioModel>()), Times.Once);

    }
    
    
}