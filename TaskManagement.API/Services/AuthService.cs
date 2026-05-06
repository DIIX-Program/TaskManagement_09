using AutoMapper;
using TaskManagement.API.DTOs.Auth;
using TaskManagement.API.Helpers;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (await _userRepository.ExistsAsync(request.Email))
            {
                return new AuthResponseDto { Success = false, Message = "Email already exists." };
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = PasswordHasher.HashPassword(request.Password);
            user.RoleId = 3; // Default Employee

            await _userRepository.AddAsync(user);

            // Re-fetch to get role name
            var createdUser = await _userRepository.GetByIdAsync(user.UserId);

            return new AuthResponseDto
            {
                Success = true,
                UserId = createdUser.UserId,
                FullName = createdUser.FullName,
                Role = createdUser.Role?.RoleName ?? "Employee",
                AvatarUrl = createdUser.AvatarUrl,
                Message = "Registration successful."
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return new AuthResponseDto { Success = false, Message = "Invalid email or password." };
            }

            return new AuthResponseDto
            {
                Success = true,
                UserId = user.UserId,
                FullName = user.FullName,
                Role = user.Role?.RoleName ?? "Employee",
                AvatarUrl = user.AvatarUrl,
                Message = "Login successful."
            };
        }
    }
}
