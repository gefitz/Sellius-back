namespace Sellius.API.Domain.Traces;

public static class EnterpriseTrace
{
    public const string NotFound          = "Empresa não encontrada.";
    public const string AlreadyExists     = "Já existe uma empresa cadastrada com este documento.";
    public const string CreateFailed      = "Falha ao cadastrar empresa.";
    public const string UpdateFailed      = "Falha ao atualizar empresa.";
    public const string InactivateFailed  = "Falha ao inativar empresa.";
    public const string CreateUserFailed  = "Empresa cadastrada, mas falha ao criar o usuário administrador.";
}
