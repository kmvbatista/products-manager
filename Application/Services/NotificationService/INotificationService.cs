using Domain.DomainNotifications;
using System.Collections.Generic;

namespace Application.Services.NotificationService
{
  public interface INotificationService
  {
    void AddNotification(string key, string value);
    void AddEntityNotifications(FluentValidation.Results.ValidationResult validationResult);
    bool HasNotifications();
    IReadOnlyCollection<Notification> GetNotifications();
  }
}
