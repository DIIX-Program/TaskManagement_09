namespace TaskManagement.API.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequestDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string AvatarUrl { get; set; }
        public string Message { get; set; }
    }
}
