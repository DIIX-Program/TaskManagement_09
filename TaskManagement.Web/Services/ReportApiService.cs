using System.Text.Json;
using TaskManagement.Web.Models.ViewModels;

namespace TaskManagement.Web.Services
{
    public class ReportApiService : ApiService
    {
        public ReportApiService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<UserWorkloadViewModel>>> GetWorkloadReportAsync()
        {
            var response = await _httpClient.GetAsync("Reports/workload");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<IEnumerable<UserWorkloadViewModel>>>(result, _options)!;
        }

        public async Task<ApiResponse<ProjectReportViewModel>> GetProjectReportAsync(int projectId)
        {
            var response = await _httpClient.GetAsync($"Reports/project/{projectId}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<ProjectReportViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<object>> SendReportAsync(int projectId, int senderId, int receiverId, string title, string content)
        {
            var dto = new { ProjectId = projectId, ReceiverId = receiverId, Title = title, Content = content };
            var response = await _httpClient.PostAsJsonAsync($"Reports/messages?senderId={senderId}", dto);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<object>>(result, _options)!;
        }

        public async Task<ApiResponse<IEnumerable<ReportMessageViewModel>>> GetMessagesAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"Reports/messages/user/{userId}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<IEnumerable<ReportMessageViewModel>>>(result, _options)!;
        }
    }

    public class ProjectReportViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int Progress { get; set; }
        public List<TaskStatusCountViewModel> TaskDistribution { get; set; }
    }

    public class TaskStatusCountViewModel
    {
        public string StatusName { get; set; }
        public int Count { get; set; }
    }

    public class ReportMessageViewModel
    {
        public int ReportId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserWorkloadViewModel
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
