using TaskManagement.API.DTOs.Auth;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IAuthService
    {
        System.Threading.Tasks.Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        System.Threading.Tasks.Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
}
