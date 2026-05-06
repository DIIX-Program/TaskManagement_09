using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Models.Entities
{
    public class TaskStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; }

        // Navigation properties
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
