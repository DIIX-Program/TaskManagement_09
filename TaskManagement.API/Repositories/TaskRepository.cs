using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Status)
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetByAssigneeIdAsync(int userId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Status)
                .Where(t => t.AssignedTo == userId && !t.IsDeleted)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetByMemberUserIdAsync(int userId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Status)
                .Where(t => !t.IsDeleted && t.Project.ProjectMembers.Any(pm => pm.UserId == userId))
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetByProjectIdAsync(int projectId)
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.Status)
                .Where(t => t.ProjectId == projectId && !t.IsDeleted)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<Models.Entities.Task?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.TaskId == id && !t.IsDeleted);
        }

        public async System.Threading.Tasks.Task AddAsync(Models.Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Models.Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<bool> StatusExistsAsync(int statusId)
        {
            return await _context.TaskStatuses.AnyAsync(s => s.StatusId == statusId);
        }
    }
}
