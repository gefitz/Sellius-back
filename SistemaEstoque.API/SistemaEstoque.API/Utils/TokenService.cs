using System.Security.Claims;

namespace Sellius.API.Utils;

public static class TokenService
{
    public static int RecuperaIdEmpresa(ClaimsPrincipal identity) =>
        int.Parse(identity.FindFirst("empresa")!.Value);

    public static int RecuperaIdUsuario(ClaimsPrincipal identity) =>
        int.Parse(identity.FindFirst("id")!.Value);
}
