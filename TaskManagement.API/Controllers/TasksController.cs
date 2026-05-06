using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DTOs.Task;
using TaskManagement.API.Helpers;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaskResponseDto>>>> GetTasks([FromQuery] int? projectId, [FromQuery] int? userId = null, [FromQuery] string? role = null)
        {
            IEnumerable<TaskResponseDto> tasks;
            if (projectId.HasValue)
                tasks = await _taskService.GetTasksByProjectAsync(projectId.Value);
            else
                tasks = await _taskService.GetAllTasksAsync(userId, role);

            return Ok(new ApiResponse<IEnumerable<TaskResponseDto>>(true, "Tasks retrieved", tasks));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TaskResponseDto>>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound(new ApiResponse<TaskResponseDto>(false, "Task not found"));

            return Ok(new ApiResponse<TaskResponseDto>(true, "Task retrieved", task));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TaskResponseDto>>> CreateTask(CreateTaskDto request, [FromQuery] int? userId = null, [FromQuery] string? role = null)
        {
            try
            {
                var task = await _taskService.CreateTaskAsync(request, userId, role);
                return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, new ApiResponse<TaskResponseDto>(true, "Task created", task));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TaskResponseDto>(false, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TaskResponseDto>>> UpdateTask(int id, UpdateTaskDto request, [FromQuery] int? userId = null, [FromQuery] string? role = null)
        {
            try
            {
                var task = await _taskService.UpdateTaskAsync(id, request, userId, role);
                if (task == null)
                    return NotFound(new ApiResponse<TaskResponseDto>(false, "Task not found"));

                return Ok(new ApiResponse<TaskResponseDto>(true, "Task updated", task));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TaskResponseDto>(false, ex.Message));
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateStatus(int id, UpdateTaskStatusDto request)
        {
            var result = await _taskService.UpdateTaskStatusAsync(id, request.StatusId);
            if (!result)
                return BadRequest(new ApiResponse<object>(false, "Failed to update status. Status may not exist or task not found."));

            return Ok(new ApiResponse<object>(true, "Status updated"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
                return NotFound(new ApiResponse<object>(false, "Task not found"));

            return Ok(new ApiResponse<object>(true, "Task deleted"));
        }
    }
}
