using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Services;
using TaskManagement.Web.Models.ViewModels;

namespace TaskManagement.Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly UserApiService _userService;
        private readonly AuthApiService _authService;

        public TeamController(UserApiService userService, AuthApiService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Team Members";
            var response = await _userService.GetAllAsync();
            return View(response.Data ?? new List<UserViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var response = await _userService.GetAllAsync();
            return Json(response.Data ?? new List<UserViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Invite(string email)
        {
            // Auto register a new employee with default password
            var model = new RegisterViewModel
            {
                Email = email,
                FullName = email.Split('@')[0], // Default name from email
                Password = "Starbucks@123",
                ConfirmPassword = "Starbucks@123",
                RoleName = "Employee"
            };

            var response = await _authService.RegisterAsync(model);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(int userId, string roleName)
        {
            var response = await _userService.UpdateRoleAsync(userId, roleName);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }
    }
}
