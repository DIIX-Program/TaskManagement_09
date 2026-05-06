using System.Text;
using System.Text.Json;
using TaskManagement.Web.Models.ViewModels;

namespace TaskManagement.Web.Services
{
    public class TaskApiService : ApiService
    {
        public TaskApiService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<TaskResponseViewModel>>> GetAllAsync(int? projectId = null, int? userId = null, string? role = null)
        {
            var url = "Tasks?";
            if (projectId.HasValue) url += $"projectId={projectId}&";
            if (userId.HasValue) url += $"userId={userId}&";
            if (!string.IsNullOrEmpty(role)) url += $"role={role}&";
            
            var response = await _httpClient.GetAsync(url.TrimEnd('&', '?'));
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<IEnumerable<TaskResponseViewModel>>>(result, _options)!;
        }

        public async Task<ApiResponse<TaskResponseViewModel>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Tasks/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<TaskResponseViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<TaskResponseViewModel>> CreateAsync(CreateTaskViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Tasks", content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<TaskResponseViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<TaskResponseViewModel>> UpdateAsync(int id, CreateTaskViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Tasks/{id}", content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<TaskResponseViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<object>> UpdateStatusAsync(int id, int statusId)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { statusId }, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"Tasks/{id}/status", content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<object>>(result, _options)!;
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Tasks/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<bool>>(result, _options)!;
        }
    }

    public class TaskResponseViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int? AssignedTo { get; set; }
        public string? AssigneeName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public byte Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateTaskViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedTo { get; set; }
        public int StatusId { get; set; } = 1; // Default To Do
        public byte Priority { get; set; } = 2; // Default Medium
        public DateTime? Deadline { get; set; }
    }
}
