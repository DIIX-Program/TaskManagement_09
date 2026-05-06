using TaskManagement.API.DTOs.Project;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IProjectService
    {
        System.Threading.Tasks.Task<IEnumerable<ProjectResponseDto>> GetAllProjectsAsync(int? userId = null, string? role = null);
        System.Threading.Tasks.Task<ProjectResponseDto?> GetProjectByIdAsync(int id);
        System.Threading.Tasks.Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto request);
        System.Threading.Tasks.Task<ProjectResponseDto?> UpdateProjectAsync(int id, UpdateProjectDto request, int? userId = null, string? role = null);
        System.Threading.Tasks.Task<bool> DeleteProjectAsync(int id);
        System.Threading.Tasks.Task<bool> AddMemberAsync(int projectId, int userId, int? requestorId = null, string? requestorRole = null);
        System.Threading.Tasks.Task<bool> AddMemberByEmailAsync(int projectId, string email, int? requestorId = null, string? requestorRole = null);
        System.Threading.Tasks.Task<bool> RemoveMemberAsync(int projectId, int userId);
        System.Threading.Tasks.Task<IEnumerable<ProjectMemberDto>> GetProjectMembersAsync(int projectId);
    }

    public class ProjectMemberDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
