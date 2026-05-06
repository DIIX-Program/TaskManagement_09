using TaskManagement.API.Models.Entities;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        System.Threading.Tasks.Task<IEnumerable<Project>> GetAllAsync();
        System.Threading.Tasks.Task<IEnumerable<Project>> GetByUserIdAsync(int userId);
        System.Threading.Tasks.Task<Project?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(Project project);
        System.Threading.Tasks.Task UpdateAsync(Project project);
        System.Threading.Tasks.Task DeleteAsync(int id);
        System.Threading.Tasks.Task AddMemberAsync(ProjectMember member);
        System.Threading.Tasks.Task RemoveMemberAsync(int projectId, int userId);
        System.Threading.Tasks.Task<bool> IsMemberAsync(int projectId, int userId);
        System.Threading.Tasks.Task<IEnumerable<ProjectMember>> GetMembersAsync(int projectId);
    }
}
