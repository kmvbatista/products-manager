using System.Collections.Generic;
using FluentValidation.Results;
using Domain.DomainNotifications;

namespace Application.Services.NotificationService
{
  public class NotificationService : INotificationService
  {
    private readonly NotificationContext _notificationContext;
    public NotificationService(NotificationContext notificationContext)
    {
      _notificationContext = notificationContext;
    }

    public void AddNotification(string key, string value)
    {
      _notificationContext.AddNotification(key, value);
    }

    public void AddEntityNotifications(ValidationResult validationResult)
    {
      _notificationContext.AddNotifications(validationResult);
    }

    public bool HasNotifications()
    {
      return _notificationContext.HasNotifications;
    }

    public IReadOnlyCollection<Notification> GetNotifications()
    {
      return _notificationContext.Notifications;
    }
  }
}
