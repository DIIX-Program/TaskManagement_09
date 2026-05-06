using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly NotificationApiService _notificationService;

        public NotificationsController(NotificationApiService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Notifications";
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var response = await _notificationService.GetByUserIdAsync(userId);
            return View(response.Data ?? new List<NotificationViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> GetRecent()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var response = await _notificationService.GetByUserIdAsync(userId);
            var recent = response.Data?.Where(n => !n.IsRead).OrderByDescending(n => n.CreatedAt).Take(5);
            return Json(recent ?? new List<NotificationViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] NotificationViewModel model)
        {
            await _notificationService.CreateAsync(model.UserId, model.Message);
            return Ok();
        }
    }
}
