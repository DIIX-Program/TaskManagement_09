using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.API.Models.Entities
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public int? AssignedTo { get; set; }

        [Required]
        public int StatusId { get; set; }

        public byte Priority { get; set; } = 2; // 1: Low, 2: Medium, 3: High

        public DateTime? Deadline { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual User? Assignee { get; set; }

        [ForeignKey("StatusId")]
        public virtual TaskStatus Status { get; set; }
    }
}
