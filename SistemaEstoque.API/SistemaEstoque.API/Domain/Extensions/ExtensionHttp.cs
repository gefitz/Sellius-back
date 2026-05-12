using System.Security.Claims;

namespace Sellius.API.Domain.Extensions;

public static class ExtensionHttp
{
    public static Guid GetEnterpriseId(this ClaimsPrincipal request) =>
        new(request.FindFirst("empresa")!.Value);

    public static Guid GetUserId(this ClaimsPrincipal request) =>
        new(request.FindFirst("id")!.Value);
}