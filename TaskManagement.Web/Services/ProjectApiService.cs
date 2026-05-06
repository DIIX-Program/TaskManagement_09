using System.Text;
using System.Text.Json;
using TaskManagement.Web.Models.ViewModels;

namespace TaskManagement.Web.Services
{
    public class ProjectApiService : ApiService
    {
        public ProjectApiService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<ProjectViewModel>>> GetAllAsync(int? userId = null, string? role = null)
        {
            var url = "Projects";
            if (userId.HasValue && !string.IsNullOrEmpty(role))
                url += $"?userId={userId}&role={role}";

            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<IEnumerable<ProjectViewModel>>>(result, _options)!;
        }

        public async Task<ApiResponse<ProjectViewModel>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Projects/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<ProjectViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<ProjectViewModel>> CreateAsync(ProjectViewModel model, string? role = null)
        {
            var url = "Projects";
            if (!string.IsNullOrEmpty(role))
                url += $"?role={role}";

            var content = new StringContent(JsonSerializer.Serialize(model, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<ProjectViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<object>> AddMemberByEmailAsync(int projectId, string email, int? requestorId = null, string? role = null)
        {
            var url = $"Projects/{projectId}/members/email";
            if (requestorId.HasValue && !string.IsNullOrEmpty(role))
                url += $"?requestorId={requestorId}&role={role}";

            var content = new StringContent(JsonSerializer.Serialize(email, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<object>>(result, _options)!;
        }

        public async Task<ApiResponse<ProjectViewModel>> UpdateAsync(int id, ProjectViewModel model, int? userId = null, string? role = null)
        {
            var url = $"Projects/{id}";
            if (userId.HasValue && !string.IsNullOrEmpty(role))
                url += $"?userId={userId}&role={role}";

            var content = new StringContent(JsonSerializer.Serialize(model, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<ProjectViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<IEnumerable<ProjectMemberViewModel>>> GetMembersAsync(int projectId)
        {
            var response = await _httpClient.GetAsync($"Projects/{projectId}/members");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<IEnumerable<ProjectMemberViewModel>>>(result, _options)!;
        }

        public async Task<ApiResponse<bool>> AddMemberAsync(int projectId, int userId, int? requestorId = null, string? role = null)
        {
            var url = $"Projects/{projectId}/members/{userId}";
            if (requestorId.HasValue && !string.IsNullOrEmpty(role))
                url += $"?requestorId={requestorId}&role={role}";

            var response = await _httpClient.PostAsync(url, null);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<bool>>(result, _options)!;
        }

        public async Task<ApiResponse<bool>> RemoveMemberAsync(int projectId, int userId)
        {
            var response = await _httpClient.DeleteAsync($"Projects/{projectId}/members/{userId}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<bool>>(result, _options)!;
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Projects/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<bool>>(result, _options)!;
        }
    }

    public class ProjectMemberViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }
    }

    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MemberCount { get; set; }
        public int Progress { get; set; }
        public IEnumerable<ProjectMemberViewModel>? Members { get; set; }
        public IEnumerable<TaskResponseViewModel>? Tasks { get; set; }
    }
}
