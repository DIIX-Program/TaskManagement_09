using TaskManagement.API.Models.Entities;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetAllAsync();
        System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetByAssigneeIdAsync(int userId);
        System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetByMemberUserIdAsync(int userId);
        System.Threading.Tasks.Task<IEnumerable<Models.Entities.Task>> GetByProjectIdAsync(int projectId);
        System.Threading.Tasks.Task<Models.Entities.Task?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(Models.Entities.Task task);
        System.Threading.Tasks.Task UpdateAsync(Models.Entities.Task task);
        System.Threading.Tasks.Task DeleteAsync(int id);
        System.Threading.Tasks.Task<bool> StatusExistsAsync(int statusId);
    }
}
