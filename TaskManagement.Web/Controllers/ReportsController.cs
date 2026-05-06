using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ReportApiService _reportService;

        public ReportsController(ReportApiService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return Forbid();

            ViewData["Title"] = "Báo cáo nhân sự";
            var response = await _reportService.GetWorkloadReportAsync();
            
            return View(response.Data ?? new List<UserWorkloadViewModel>());
        }

        public async Task<IActionResult> Messages()
        {
            ViewData["Title"] = "Tin nhắn báo cáo";
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var response = await _reportService.GetMessagesAsync(userId);
            return View(response.Data ?? new List<ReportMessageViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Send(int projectId, int receiverId, string title, string content)
        {
            var senderId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var response = await _reportService.SendReportAsync(projectId, senderId, receiverId, title, content);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }
    }
}
