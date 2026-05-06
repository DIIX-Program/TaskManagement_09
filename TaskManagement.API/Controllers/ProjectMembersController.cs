using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Helpers;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/projects/{projectId}/members")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectMembersController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectMemberDto>>>> GetMembers(int projectId)
        {
            var members = await _projectService.GetProjectMembersAsync(projectId);
            return Ok(new ApiResponse<IEnumerable<ProjectMemberDto>>(true, "Members retrieved", members));
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<ApiResponse<object>>> AddMember(int projectId, int userId, [FromQuery] int? requestorId = null, [FromQuery] string? role = null)
        {
            try
            {
                var result = await _projectService.AddMemberAsync(projectId, userId, requestorId, role);
                if (!result)
                    return BadRequest(new ApiResponse<object>(false, "Cộng sự đã tham gia dự án hoặc dữ liệu không hợp lệ"));

                return Ok(new ApiResponse<object>(true, "Thêm cộng sự thành công"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpPost("email")]
        public async Task<ActionResult<ApiResponse<object>>> AddMemberByEmail(int projectId, [FromBody] string email, [FromQuery] int? requestorId = null, [FromQuery] string? role = null)
        {
            try
            {
                var result = await _projectService.AddMemberByEmailAsync(projectId, email, requestorId, role);
                if (!result)
                    return BadRequest(new ApiResponse<object>(false, "Không tìm thấy người dùng hoặc đã tham gia dự án"));

                return Ok(new ApiResponse<object>(true, "Thêm cộng sự thành công"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<ApiResponse<object>>> RemoveMember(int projectId, int userId)
        {
            var result = await _projectService.RemoveMemberAsync(projectId, userId);
            if (!result)
                return NotFound(new ApiResponse<object>(false, "Member not found"));

            return Ok(new ApiResponse<object>(true, "Member removed successfully"));
        }
    }
}
