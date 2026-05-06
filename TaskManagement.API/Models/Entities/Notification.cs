using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.API.Models.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Message { get; set; }

        public NotificationType Type { get; set; } = NotificationType.General;

        public bool IsPriority { get; set; } = false;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

    public enum NotificationType
    {
        General = 0,
        TaskAssigned = 1,
        StatusChanged = 2,
        ProjectAdded = 3,
        DeadlineApproaching = 4
    }
}
