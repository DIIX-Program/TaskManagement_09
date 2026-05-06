using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id && !u.IsDeleted);
        }

        public async System.Threading.Tasks.Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async System.Threading.Tasks.Task UpdateAvatarAsync(int userId, string avatarUrl)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.AvatarUrl = avatarUrl;
                await _context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task UpdateNotificationPreferencesAsync(int userId, List<UserNotificationPreference> preferences)
        {
            var existing = await _context.UserNotificationPreferences.Where(p => p.UserId == userId).ToListAsync();
            _context.UserNotificationPreferences.RemoveRange(existing);
            
            await _context.UserNotificationPreferences.AddRangeAsync(preferences);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateRoleAsync(int userId, int roleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.RoleId = roleId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
