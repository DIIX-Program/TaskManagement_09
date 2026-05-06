using System.Net.Http.Headers;
using System.Text.Json;

namespace TaskManagement.Web.Services
{
    public class ApiService
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://127.0.0.1:5129/api/"); // Use IP to avoid localhost resolution issues
            _options = new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
        }
    }
}
