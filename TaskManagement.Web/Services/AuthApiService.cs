using System.Text;
using System.Text.Json;
using TaskManagement.Web.Models.ViewModels;

namespace TaskManagement.Web.Services
{
    public class AuthApiService : ApiService
    {
        public AuthApiService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<AuthResponseViewModel>> LoginAsync(LoginViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Auth/login", content);
            var result = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = "Login failed";
                try {
                   var err = JsonSerializer.Deserialize<ApiResponse<AuthResponseViewModel>>(result, _options);
                   if (err != null) errorMsg = err.Message;
                } catch {}
                return new ApiResponse<AuthResponseViewModel> { Success = false, Message = errorMsg };
            }

            return JsonSerializer.Deserialize<ApiResponse<AuthResponseViewModel>>(result, _options)!;
        }

        public async Task<ApiResponse<AuthResponseViewModel>> RegisterAsync(RegisterViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Auth/register", content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<AuthResponseViewModel>>(result, _options)!;
        }
    }
}
