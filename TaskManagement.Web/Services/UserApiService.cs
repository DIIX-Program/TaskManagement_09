using System.Text.Json;
using TaskManagement.Web.Models.ViewModels;

namespace TaskManagement.Web.Services
{
    public class UserApiService : ApiService
    {
        public UserApiService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<UserViewModel>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Users");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<IEnumerable<UserViewModel>>>(result, _options)!;
        }

        public async Task<ApiResponse<UserViewModel>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Users/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<UserViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<string>> UploadAvatarAsync(int userId, Stream fileStream, string fileName, string contentType)
        {
            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            content.Add(fileContent, "file", fileName);

            var response = await _httpClient.PostAsync($"Users/{userId}/avatar", content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<string>>(result, _options)!;
        }

        public async Task<ApiResponse<object>> UpdatePreferencesAsync(int userId, List<NotificationPreferenceViewModel> preferences)
        {
            var content = new StringContent(JsonSerializer.Serialize(preferences, _options), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Users/{userId}/preferences", content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<object>>(result, _options)!;
        }

        public async Task<ApiResponse<object>> UpdateRoleAsync(int userId, string roleName)
        {
            var response = await _httpClient.PatchAsync($"Users/{userId}/role?roleName={roleName}", null);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<object>>(result, _options)!;
        }
    }

    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class NotificationPreferenceViewModel
    {
        public int Type { get; set; }
        public bool IsPriority { get; set; }
    }
}
