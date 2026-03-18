using Sellius.API.Models.Usuario;

namespace Sellius.Test.BuildModels;

public class UsuarioBuildModel
{
    private int _id = 1;
    private string _nome = "Geovane";
    private string _documento = "09563364937";
    private string _email = "almeidafitz@gmail.com";
    private int _cidadeId = 1;
    private string _cep = "83409370";
    private string _rua = "Julio Ribeiro 392";
    private DateTime _dtCadastro = DateTime.UtcNow;
    private int _empresaId = 1;
    private int _idTpUsuario = 1;
    private short _fAtivo = 1;

    public static UsuarioBuildModel NewObject => new();

    public UsuarioModel Build() => new()
    {   
        id = _id,
        Nome =  _nome,
        Documento = _documento,
        dthCadastro =  _dtCadastro,
        EmpresaId = _empresaId,
        IdTpUsuario = _idTpUsuario,
        fAtivo = _fAtivo,
        CidadeId =  _cidadeId,
        CEP = _cep,
        Rua = _rua,
        Email = _email
        
    };
}