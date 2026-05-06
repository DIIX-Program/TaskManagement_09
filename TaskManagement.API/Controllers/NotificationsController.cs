using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DTOs.Notification;
using TaskManagement.API.Helpers;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<NotificationDto>>>> GetUserNotifications(int userId)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return Ok(new ApiResponse<IEnumerable<NotificationDto>>(true, "Notifications retrieved", notifications));
        }

        [HttpPatch("{id}/read")]
        public async Task<ActionResult<ApiResponse<object>>> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok(new ApiResponse<object>(true, "Notification marked as read"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> CreateNotification([FromBody] CreateNotificationDto request)
        {
            await _notificationService.CreateNotificationAsync(request.UserId, request.Message);
            return Ok(new ApiResponse<object>(true, "Notification created"));
        }
    }

    public class CreateNotificationDto
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
