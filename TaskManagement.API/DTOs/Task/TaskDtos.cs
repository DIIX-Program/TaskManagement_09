namespace TaskManagement.API.DTOs.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedTo { get; set; }
        public int StatusId { get; set; }
        public byte Priority { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? AssignedTo { get; set; }
        public int StatusId { get; set; }
        public byte Priority { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public class UpdateTaskStatusDto
    {
        public int StatusId { get; set; }
    }

    public class TaskResponseDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? AssignedTo { get; set; }
        public string? AssigneeName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public byte Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
