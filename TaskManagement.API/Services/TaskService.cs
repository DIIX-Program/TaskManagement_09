using AutoMapper;
using TaskManagement.API.DTOs.Task;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            INotificationService notificationService,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(int? userId = null, string? role = null)
        {
            IEnumerable<Models.Entities.Task> tasks;
            if (role == "Employee" && userId.HasValue)
            {
                tasks = await _taskRepository.GetByMemberUserIdAsync(userId.Value);
            }
            else
            {
                tasks = await _taskRepository.GetAllAsync();
            }
            return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
        }

        public async Task<IEnumerable<TaskResponseDto>> GetTasksByProjectAsync(int projectId)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskResponseDto>(task);
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto request, int? requestorId = null, string? requestorRole = null)
        {
            // Permission Check: Manager/Employee can only create tasks in projects they belong to
            if (requestorRole != "Admin" && requestorId.HasValue)
            {
                if (!await _projectRepository.IsMemberAsync(request.ProjectId, requestorId.Value))
                    throw new UnauthorizedAccessException("Bạn không có quyền thêm công việc vào dự án này vì không phải thành viên.");
            }

            // Business Rule: Assigned user must belong to project
            if (request.AssignedTo.HasValue)
            {
                var isMember = await _projectRepository.IsMemberAsync(request.ProjectId, request.AssignedTo.Value);
                if (!isMember)
                    throw new Exception("Người thực hiện phải là thành viên của dự án.");
            }

            var task = _mapper.Map<Models.Entities.Task>(request);
            await _taskRepository.AddAsync(task);

            // Notification
            if (task.AssignedTo.HasValue)
            {
                await _notificationService.CreateNotificationAsync(task.AssignedTo.Value, $"You have been assigned to task: {task.Title}");
            }

            var createdTask = await _taskRepository.GetByIdAsync(task.TaskId);
            return _mapper.Map<TaskResponseDto>(createdTask);
        }

        public async Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto request, int? requestorId = null, string? requestorRole = null)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            // Permission Check: Manager/Employee can only update tasks in projects they belong to
            if (requestorRole != "Admin" && requestorId.HasValue)
            {
                if (!await _projectRepository.IsMemberAsync(task.ProjectId, requestorId.Value))
                    throw new UnauthorizedAccessException("Bạn không có quyền chỉnh sửa công việc này vì không phải thành viên dự án.");
            }

            // Business Rule: Assigned user must belong to project
            if (request.AssignedTo.HasValue && request.AssignedTo != task.AssignedTo)
            {
                var isMember = await _projectRepository.IsMemberAsync(task.ProjectId, request.AssignedTo.Value);
                if (!isMember)
                    throw new Exception("Người thực hiện phải là thành viên của dự án.");
                
                await _notificationService.CreateNotificationAsync(request.AssignedTo.Value, $"Bạn đã được giao nhiệm vụ mới: {request.Title}");
            }

            _mapper.Map(request, task);
            await _taskRepository.UpdateAsync(task);

            return _mapper.Map<TaskResponseDto>(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            await _taskRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> UpdateTaskStatusAsync(int id, int statusId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            // Business Rule: Validate status exists
            if (!await _taskRepository.StatusExistsAsync(statusId))
                return false;

            task.StatusId = statusId;
            await _taskRepository.UpdateAsync(task);

            if (task.AssignedTo.HasValue)
            {
                await _notificationService.CreateNotificationAsync(task.AssignedTo.Value, $"Task status updated: {task.Title}");
            }

            return true;
        }
    }
}
