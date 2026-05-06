using AutoMapper;
using TaskManagement.API.DTOs.Project;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectResponseDto>> GetAllProjectsAsync(int? userId = null, string? role = null)
        {
            IEnumerable<Project> projects;
            if (role == "Employee" && userId.HasValue)
            {
                projects = await _projectRepository.GetByUserIdAsync(userId.Value);
            }
            else
            {
                projects = await _projectRepository.GetAllAsync();
            }
            return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
        }

        public async Task<ProjectResponseDto?> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectResponseDto>(project);
        }

        public async Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto request)
        {
            var project = _mapper.Map<Project>(request);
            await _projectRepository.AddAsync(project);

            // Business Rule: Auto add creator to ProjectMembers
            await _projectRepository.AddMemberAsync(new ProjectMember
            {
                ProjectId = project.ProjectId,
                UserId = request.CreatedBy
            });

            var createdProject = await _projectRepository.GetByIdAsync(project.ProjectId);
            return _mapper.Map<ProjectResponseDto>(createdProject);
        }

        public async Task<ProjectResponseDto?> UpdateProjectAsync(int id, UpdateProjectDto request, int? userId = null, string? role = null)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return null;

            // Permission Check: Manager can only update projects they are member of
            if (role == "Manager" && userId.HasValue)
            {
                if (!await _projectRepository.IsMemberAsync(id, userId.Value))
                    throw new UnauthorizedAccessException("Bạn không có quyền chỉnh sửa dự án này vì không phải thành viên.");
            }

            _mapper.Map(request, project);
            await _projectRepository.UpdateAsync(project);

            return _mapper.Map<ProjectResponseDto>(project);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return false;

            await _projectRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> AddMemberAsync(int projectId, int userId, int? requestorId = null, string? requestorRole = null)
        {
            // Permission Check: Manager can only add members to projects they are member of
            if (requestorRole == "Manager" && requestorId.HasValue)
            {
                if (!await _projectRepository.IsMemberAsync(projectId, requestorId.Value))
                    throw new UnauthorizedAccessException("Bạn không có quyền thêm cộng sự vào dự án này vì không phải thành viên.");
            }

            if (await _projectRepository.IsMemberAsync(projectId, userId))
                return false; // Already a member

            await _projectRepository.AddMemberAsync(new ProjectMember
            {
                ProjectId = projectId,
                UserId = userId
            });
            return true;
        }

        public async Task<bool> AddMemberByEmailAsync(int projectId, string email, int? requestorId = null, string? requestorRole = null)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return false;

            return await AddMemberAsync(projectId, user.UserId, requestorId, requestorRole);
        }

        public async Task<bool> RemoveMemberAsync(int projectId, int userId)
        {
            if (!await _projectRepository.IsMemberAsync(projectId, userId))
                return false;

            await _projectRepository.RemoveMemberAsync(projectId, userId);
            return true;
        }

        public async Task<IEnumerable<ProjectMemberDto>> GetProjectMembersAsync(int projectId)
        {
            var members = await _projectRepository.GetMembersAsync(projectId);
            return members.Select(m => new ProjectMemberDto
            {
                UserId = m.UserId,
                FullName = m.User.FullName,
                Email = m.User.Email,
                JoinedAt = m.JoinedAt
            });
        }
    }
}
