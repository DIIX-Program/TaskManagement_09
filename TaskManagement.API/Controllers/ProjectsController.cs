using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DTOs.Project;
using TaskManagement.API.Helpers;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectResponseDto>>>> GetProjects([FromQuery] int? userId = null, [FromQuery] string? role = null)
        {
            var projects = await _projectService.GetAllProjectsAsync(userId, role);
            return Ok(new ApiResponse<IEnumerable<ProjectResponseDto>>(true, "Projects retrieved", projects));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> GetProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound(new ApiResponse<ProjectResponseDto>(false, "Project not found"));

            return Ok(new ApiResponse<ProjectResponseDto>(true, "Project retrieved", project));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> CreateProject(CreateProjectDto request, [FromQuery] string role)
        {
            if (role == "Employee")
                return Forbid();

            var project = await _projectService.CreateProjectAsync(request);
            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, new ApiResponse<ProjectResponseDto>(true, "Project created", project));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectResponseDto>>> UpdateProject(int id, UpdateProjectDto request, [FromQuery] int? userId = null, [FromQuery] string? role = null)
        {
            try
            {
                var project = await _projectService.UpdateProjectAsync(id, request, userId, role);
                if (project == null)
                    return NotFound(new ApiResponse<ProjectResponseDto>(false, "Project not found"));

                return Ok(new ApiResponse<ProjectResponseDto>(true, "Project updated", project));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result)
                return NotFound(new ApiResponse<object>(false, "Project not found"));

            return Ok(new ApiResponse<object>(true, "Project deleted"));
        }
    }
}
