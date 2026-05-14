using Sellius.API.Domain.Models;

namespace Sellius.API.Domain.Handler.Notification;

public class NotificationHandler
{
    public static NotificationError HandleException(Exception exception) => new()
    {
        Key = exception.GetType().Name,
        Message = exception.Message
    };
}
