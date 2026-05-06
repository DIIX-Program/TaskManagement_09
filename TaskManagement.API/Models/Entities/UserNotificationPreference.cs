using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.API.Models.Entities
{
    public class UserNotificationPreference
    {
        [Key]
        public int PreferenceId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public NotificationType NotificationType { get; set; }

        public bool IsPriority { get; set; } = false;

        // Navigation property
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
