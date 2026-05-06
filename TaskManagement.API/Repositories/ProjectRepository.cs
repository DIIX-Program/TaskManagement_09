using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ProjectMembers)
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Project>> GetByUserIdAsync(int userId)
        {
            return await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ProjectMembers)
                .Where(p => !p.IsDeleted && p.ProjectMembers.Any(pm => pm.UserId == userId))
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectId == id && !p.IsDeleted);
        }

        public async System.Threading.Tasks.Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                project.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task AddMemberAsync(ProjectMember member)
        {
            await _context.ProjectMembers.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task RemoveMemberAsync(int projectId, int userId)
        {
            var member = await _context.ProjectMembers
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
            if (member != null)
            {
                _context.ProjectMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<bool> IsMemberAsync(int projectId, int userId)
        {
            return await _context.ProjectMembers
                .AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
        }

        public async System.Threading.Tasks.Task<IEnumerable<ProjectMember>> GetMembersAsync(int projectId)
        {
            return await _context.ProjectMembers
                .Include(pm => pm.User)
                .Where(pm => pm.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
