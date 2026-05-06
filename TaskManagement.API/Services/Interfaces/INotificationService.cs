using TaskManagement.API.DTOs.Notification;

namespace TaskManagement.API.Services.Interfaces
{
    public interface INotificationService
    {
        System.Threading.Tasks.Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(int userId);
        System.Threading.Tasks.Task CreateNotificationAsync(int userId, string message);
        System.Threading.Tasks.Task MarkAsReadAsync(int notificationId);
    }
}
