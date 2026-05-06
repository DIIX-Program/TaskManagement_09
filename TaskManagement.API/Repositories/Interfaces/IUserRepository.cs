using TaskManagement.API.Models.Entities;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync();
        System.Threading.Tasks.Task<User?> GetByIdAsync(int id);
        System.Threading.Tasks.Task<User?> GetByEmailAsync(string email);
        System.Threading.Tasks.Task AddAsync(User user);
        System.Threading.Tasks.Task UpdateAsync(User user);
        System.Threading.Tasks.Task DeleteAsync(int id);
        System.Threading.Tasks.Task<bool> ExistsAsync(string email);
        System.Threading.Tasks.Task UpdateAvatarAsync(int userId, string avatarUrl);
        System.Threading.Tasks.Task UpdateNotificationPreferencesAsync(int userId, List<UserNotificationPreference> preferences);
        System.Threading.Tasks.Task UpdateRoleAsync(int userId, int roleId);
    }
}
