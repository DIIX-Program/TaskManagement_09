using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ProjectApiService _projectService;
        private readonly TaskApiService _taskService;
        private readonly UserApiService _userService;

        public DashboardController(ProjectApiService projectService, TaskApiService taskService, UserApiService userService)
        {
            _projectService = projectService;
            _taskService = taskService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Dashboard Overview";
            
            var userIdStr = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");
            int? userId = string.IsNullOrEmpty(userIdStr) ? null : int.Parse(userIdStr);

            var projects = await _projectService.GetAllAsync(userId, role);
            var tasks = await _taskService.GetAllAsync(null, userId, role);
            var users = await _userService.GetAllAsync();

            ViewBag.ProjectCount = projects.Data?.Count() ?? 0;
            ViewBag.TaskCount = tasks.Data?.Count() ?? 0;
            ViewBag.TeamCount = users.Data?.Count() ?? 0;
            ViewBag.RecentTasks = tasks.Data?.Take(5) ?? new List<TaskResponseViewModel>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn()
        {
            // For now, we simulate attendance by creating a local notification or just returning success
            // In a real app, this would save to an Attendance table
            TempData["SuccessMessage"] = "Điểm danh thành công! Chúc bạn một ngày làm việc hiệu quả.";
            return RedirectToAction("Index");
        }
    }
}
