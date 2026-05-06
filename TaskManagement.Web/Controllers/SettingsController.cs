using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly UserApiService _userService;

        public SettingsController(UserApiService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Account Settings";
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            if (userId == 0) return RedirectToAction("Login", "Auth");
            
            var response = await _userService.GetByIdAsync(userId);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string fullName)
        {
            // Simplified update logic
            HttpContext.Session.SetString("FullName", fullName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
                using var stream = file.OpenReadStream();
                var response = await _userService.UploadAvatarAsync(userId, stream, file.FileName, file.ContentType);
                if (response.Success)
                {
                    HttpContext.Session.SetString("AvatarUrl", response.Data ?? "");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePreferences(List<NotificationPreferenceViewModel> preferences)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            await _userService.UpdatePreferencesAsync(userId, preferences);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeactivateAccount()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            // Call API to soft delete
            // await _userService.DeleteAsync(userId);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            // Mock password change logic
            return RedirectToAction("Index");
        }
    }
}
