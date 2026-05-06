using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Helpers;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(new ApiResponse<IEnumerable<User>>(true, "Users retrieved successfully", users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<User>(false, "User not found"));

            return Ok(new ApiResponse<User>(true, "User retrieved successfully", user));
        }

        [HttpPost("{id}/avatar")]
        public async Task<ActionResult<ApiResponse<string>>> UploadAvatar(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new ApiResponse<string>(false, "No file uploaded"));

            var fileName = $"{id}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars", fileName);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"/avatars/{fileName}";
            await _userService.UpdateAvatarAsync(id, url);

            return Ok(new ApiResponse<string>(true, "Avatar updated successfully", url));
        }

        [HttpPut("{id}/preferences")]
        public async Task<ActionResult<ApiResponse<object>>> UpdatePreferences(int id, [FromBody] List<NotificationPreferenceDto> preferences)
        {
            await _userService.UpdateNotificationPreferencesAsync(id, preferences);
            return Ok(new ApiResponse<object>(true, "Preferences updated successfully"));
        }

        [HttpPatch("{id}/role")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateRole(int id, [FromQuery] string roleName)
        {
            await _userService.UpdateUserRoleAsync(id, roleName);
            return Ok(new ApiResponse<object>(true, "User role updated successfully"));
        }
    }
}
