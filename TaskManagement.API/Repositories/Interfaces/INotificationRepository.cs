using TaskManagement.API.Models.Entities;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        System.Threading.Tasks.Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
        System.Threading.Tasks.Task AddAsync(Notification notification);
        System.Threading.Tasks.Task MarkAsReadAsync(int notificationId);
        System.Threading.Tasks.Task MarkAllAsReadAsync(int userId);
    }
}
