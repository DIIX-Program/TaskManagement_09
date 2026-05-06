namespace TaskManagement.API.DTOs.Report
{
    public class DashboardReportDto
    {
        public int TotalProjects { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int OverdueTasks { get; set; }
        public List<TaskStatusCountDto> TaskDistribution { get; set; }
    }

    public class TaskStatusCountDto
    {
        public string StatusName { get; set; }
        public int Count { get; set; }
    }

    public class TaskStatusReportDto
    {
        public string StatusName { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    public class ProjectReportDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int Progress { get; set; }
        public List<TaskStatusCountDto> TaskDistribution { get; set; }
    }

    public class TopContributorDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int TasksCompleted { get; set; }
        public int TotalTasksAssigned { get; set; }
    }

    public class SendReportDto
    {
        public int ProjectId { get; set; }
        public int ReceiverId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class ReportMessageDto
    {
        public int ReportId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
