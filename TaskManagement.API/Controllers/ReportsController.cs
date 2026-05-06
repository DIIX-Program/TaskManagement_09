using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Helpers;
using TaskManagement.API.Services.Interfaces;
using TaskManagement.API.DTOs.Report;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IReportService _reportService;

        public ReportsController(ApplicationDbContext context, IReportService reportService)
        {
            _context = context;
            _reportService = reportService;
        }

        [HttpGet("workload")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserWorkloadDto>>>> GetWorkloadReport()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.AssignedTasks)
                .Where(u => !u.IsDeleted && u.Role.RoleName == "Employee")
                .Select(u => new UserWorkloadDto
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    TotalTasks = u.AssignedTasks.Count(t => !t.IsDeleted),
                    ActiveTasks = u.AssignedTasks.Count(t => !t.IsDeleted && t.StatusId != 3), // Not Completed
                    CompletedBeforeDeadline = u.AssignedTasks.Count(t => !t.IsDeleted && t.StatusId == 3 && t.Deadline.HasValue && t.CreatedAt <= t.Deadline),
                    IsAvailable = !u.AssignedTasks.Any(t => !t.IsDeleted && t.StatusId != 3)
                })
                .ToListAsync();

            return Ok(new ApiResponse<IEnumerable<UserWorkloadDto>>(true, "Workload report retrieved", users));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<ApiResponse<ProjectReportDto>>> GetProjectReport(int projectId)
        {
            var report = await _reportService.GetProjectReportAsync(projectId);
            return Ok(new ApiResponse<ProjectReportDto>(true, "Project report retrieved", report));
        }

        [HttpGet("project/{projectId}/top-contributors")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TopContributorDto>>>> GetTopContributors(int projectId)
        {
            var contributors = await _reportService.GetTopContributorsAsync(projectId);
            return Ok(new ApiResponse<IEnumerable<TopContributorDto>>(true, "Top contributors retrieved", contributors));
        }

        [HttpPost("messages")]
        public async Task<ActionResult<ApiResponse<object>>> SendReportMessage(SendReportDto request, [FromQuery] int senderId)
        {
            await _reportService.SendReportMessageAsync(request.ProjectId, senderId, request.ReceiverId, request.Title, request.Content);
            return Ok(new ApiResponse<object>(true, "Report sent successfully"));
        }

        [HttpGet("messages/user/{userId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReportMessageDto>>>> GetUserReportMessages(int userId)
        {
            var messages = await _reportService.GetUserReportMessagesAsync(userId);
            return Ok(new ApiResponse<IEnumerable<ReportMessageDto>>(true, "Messages retrieved", messages));
        }

        [HttpPatch("messages/{id}/read")]
        public async Task<ActionResult<ApiResponse<object>>> MarkReportAsRead(int id)
        {
            await _reportService.MarkReportAsReadAsync(id);
            return Ok(new ApiResponse<object>(true, "Report marked as read"));
        }
    }

    public class UserWorkloadDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TotalTasks { get; set; }
        public int ActiveTasks { get; set; }
        public int CompletedBeforeDeadline { get; set; }
        public bool IsAvailable { get; set; }
    }
}
