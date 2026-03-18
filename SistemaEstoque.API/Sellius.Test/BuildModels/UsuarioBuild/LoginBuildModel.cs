using System.Security.Cryptography;
using System.Text;
using Sellius.API.Models.Usuario;

namespace Sellius.Test.BuildModels;

public class LoginBuildModel
{
    private int _id = 1;
    private string _email = "almeidafitz@gmail.com";
    private string password = "92253711Ge@";
    private byte[] _hash = null;
    private byte[] _salt = null;
    private bool _fEmailConfirmado = false;
    private int _usuarioID = 1;
    private int _EmpresaId = 1;

    public LoginBuildModel()
    {
        criptografar();
    }
    
    public LoginBuildModel NewObject => new();

    public LoginModel Build() =>
        new()
        {
            id = _id,
            Email = _email,
            fEmailConfirmado = _fEmailConfirmado,
            usuarioId = _usuarioID,
            EmpresaId = _EmpresaId,
            Hash =  _hash,
            Salt = _salt,
            Usuario = UsuarioBuildModel.NewObject.Build()
        };

    public void criptografar()
    {
        try
        {
            using (var hmac = new HMACSHA512())
            {
                _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                _salt = hmac.Key;
            }
            
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Falha ao criptografar a senha: " + ex.Message);
        }
    }
}