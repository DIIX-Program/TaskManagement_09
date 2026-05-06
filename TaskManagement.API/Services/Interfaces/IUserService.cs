using TaskManagement.API.Models.Entities;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IUserService
    {
        System.Threading.Tasks.Task<IEnumerable<User>> GetAllUsersAsync();
        System.Threading.Tasks.Task<User?> GetUserByIdAsync(int id);
        System.Threading.Tasks.Task<bool> UpdateAvatarAsync(int userId, string avatarUrl);
        System.Threading.Tasks.Task<bool> UpdateNotificationPreferencesAsync(int userId, List<NotificationPreferenceDto> preferences);
        System.Threading.Tasks.Task<bool> UpdateUserRoleAsync(int userId, string roleName);
    }

    public class NotificationPreferenceDto
    {
        public NotificationType Type { get; set; }
        public bool IsPriority { get; set; }
    }
}
