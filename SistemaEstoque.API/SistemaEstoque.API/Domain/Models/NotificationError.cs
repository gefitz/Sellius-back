namespace Sellius.API.Domain.Models;

public sealed record NotificationError
{
    public string Message { get; set; }
    public string Key { get; set; }
    
}