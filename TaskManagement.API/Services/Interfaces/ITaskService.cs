using TaskManagement.API.DTOs.Task;

namespace TaskManagement.API.Services.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(int? userId = null, string? role = null);
        System.Threading.Tasks.Task<IEnumerable<TaskResponseDto>> GetTasksByProjectAsync(int projectId);
        System.Threading.Tasks.Task<TaskResponseDto?> GetTaskByIdAsync(int id);
        System.Threading.Tasks.Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto request, int? userId = null, string? role = null);
        System.Threading.Tasks.Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto request, int? userId = null, string? role = null);
        System.Threading.Tasks.Task<bool> DeleteTaskAsync(int id);
        System.Threading.Tasks.Task<bool> UpdateTaskStatusAsync(int id, int statusId);
    }
}
