using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async System.Threading.Tasks.Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async System.Threading.Tasks.Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async System.Threading.Tasks.Task<bool> UpdateAvatarAsync(int userId, string avatarUrl)
        {
            await _userRepository.UpdateAvatarAsync(userId, avatarUrl);
            return true;
        }

        public async System.Threading.Tasks.Task<bool> UpdateNotificationPreferencesAsync(int userId, List<NotificationPreferenceDto> preferences)
        {
            var entities = preferences.Select(p => new UserNotificationPreference
            {
                UserId = userId,
                NotificationType = p.Type,
                IsPriority = p.IsPriority
            }).ToList();

            await _userRepository.UpdateNotificationPreferencesAsync(userId, entities);
            return true;
        }

        public async System.Threading.Tasks.Task<bool> UpdateUserRoleAsync(int userId, string roleName)
        {
            int roleId = roleName switch
            {
                "Admin" => 1,
                "Manager" => 2,
                "Employee" => 3,
                _ => 3
            };

            await _userRepository.UpdateRoleAsync(userId, roleId);
            return true;
        }
    }
}
