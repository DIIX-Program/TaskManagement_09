using TaskManagement.API.DTOs.Report;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IReportService
    {
        System.Threading.Tasks.Task<DashboardReportDto> GetDashboardStatsAsync();
        System.Threading.Tasks.Task<IEnumerable<TaskStatusReportDto>> GetTaskStatusReportAsync();
        
        // New methods for project-specific reporting and messaging
        System.Threading.Tasks.Task<ProjectReportDto> GetProjectReportAsync(int projectId);
        System.Threading.Tasks.Task<IEnumerable<TopContributorDto>> GetTopContributorsAsync(int projectId);
        System.Threading.Tasks.Task SendReportMessageAsync(int projectId, int senderId, int receiverId, string title, string content);
        System.Threading.Tasks.Task<IEnumerable<ReportMessageDto>> GetUserReportMessagesAsync(int userId);
        System.Threading.Tasks.Task MarkReportAsReadAsync(int reportId);
    }
}
