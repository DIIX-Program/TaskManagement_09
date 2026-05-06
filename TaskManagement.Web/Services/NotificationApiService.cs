using System.Text.Json;
using TaskManagement.Web.Models.ViewModels;

namespace TaskManagement.Web.Services
{
    public class NotificationApiService : ApiService
    {
        public NotificationApiService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<NotificationViewModel>>> GetByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"Notifications/user/{userId}");
            var result = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<IEnumerable<NotificationViewModel>> { Success = false, Message = "Failed to fetch notifications" };
            }
            
            return JsonSerializer.Deserialize<ApiResponse<IEnumerable<NotificationViewModel>>>(result, _options)!;
        }

        public async Task<ApiResponse<bool>> MarkAsReadAsync(int id)
        {
            var response = await _httpClient.PostAsync($"Notifications/{id}/read", null);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<bool>>(result, _options)!;
        }

        public async Task<ApiResponse<bool>> CreateAsync(int userId, string message)
        {
            var response = await _httpClient.PostAsJsonAsync("Notifications", new { userId, message });
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<bool>>(result, _options)!;
        }
    }

    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
