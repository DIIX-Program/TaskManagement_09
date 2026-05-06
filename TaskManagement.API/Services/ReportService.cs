using TaskManagement.API.DTOs.Report;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IReportRepository _reportRepository;

        public ReportService(IProjectRepository projectRepository, ITaskRepository taskRepository, IReportRepository reportRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _reportRepository = reportRepository;
        }

        public async System.Threading.Tasks.Task<DashboardReportDto> GetDashboardStatsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            var tasks = await _taskRepository.GetAllAsync();

            var now = DateTime.Now;

            return new DashboardReportDto
            {
                TotalProjects = projects.Count(),
                TotalTasks = tasks.Count(),
                CompletedTasks = tasks.Count(t => t.StatusId == 3), // Completed
                OverdueTasks = tasks.Count(t => t.Deadline.HasValue && t.Deadline.Value < now && t.StatusId != 3),
                TaskDistribution = tasks.GroupBy(t => t.Status.StatusName)
                    .Select(g => new TaskStatusCountDto
                    {
                        StatusName = g.Key,
                        Count = g.Count()
                    }).ToList()
            };
        }

        public async System.Threading.Tasks.Task<IEnumerable<TaskStatusReportDto>> GetTaskStatusReportAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var total = tasks.Count();

            if (total == 0) return new List<TaskStatusReportDto>();

            return tasks.GroupBy(t => t.Status.StatusName)
                .Select(g => new TaskStatusReportDto
                {
                    StatusName = g.Key,
                    Count = g.Count(),
                    Percentage = Math.Round((double)g.Count() / total * 100, 2)
                });
        }

        public async System.Threading.Tasks.Task<ProjectReportDto> GetProjectReportAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null) throw new Exception("Project not found");

            var tasks = project.Tasks.ToList();
            var totalTasks = tasks.Count;
            var completedTasks = tasks.Count(t => t.StatusId == 3);

            return new ProjectReportDto
            {
                ProjectId = projectId,
                ProjectName = project.Name,
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                Progress = totalTasks > 0 ? (completedTasks * 100 / totalTasks) : 0,
                TaskDistribution = tasks.GroupBy(t => t.Status.StatusName)
                    .Select(g => new TaskStatusCountDto
                    {
                        StatusName = g.Key,
                        Count = g.Count()
                    }).ToList()
            };
        }

        public async System.Threading.Tasks.Task<IEnumerable<TopContributorDto>> GetTopContributorsAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null) throw new Exception("Project not found");

            var tasks = project.Tasks.Where(t => t.AssignedTo.HasValue).ToList();
            
            return tasks.GroupBy(t => new { t.AssignedTo, t.Assignee.FullName })
                .Select(g => new TopContributorDto
                {
                    UserId = g.Key.AssignedTo!.Value,
                    FullName = g.Key.FullName,
                    TasksCompleted = g.Count(t => t.StatusId == 3),
                    TotalTasksAssigned = g.Count()
                })
                .OrderByDescending(x => x.TasksCompleted)
                .ToList();
        }

        public async System.Threading.Tasks.Task SendReportMessageAsync(int projectId, int senderId, int receiverId, string title, string content)
        {
            var report = new Report
            {
                ProjectId = projectId,
                SenderId = senderId,
                ReceiverId = receiverId,
                Title = title,
                Content = content,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            await _reportRepository.AddAsync(report);
        }

        public async System.Threading.Tasks.Task<IEnumerable<ReportMessageDto>> GetUserReportMessagesAsync(int userId)
        {
            var reports = await _reportRepository.GetByUserIdAsync(userId);
            return reports.Select(r => new ReportMessageDto
            {
                ReportId = r.ReportId,
                ProjectId = r.ProjectId,
                ProjectName = r.Project.Name,
                SenderId = r.SenderId,
                SenderName = r.Sender.FullName,
                ReceiverId = r.ReceiverId,
                ReceiverName = r.Receiver.FullName,
                Title = r.Title,
                Content = r.Content,
                IsRead = r.IsRead,
                CreatedAt = r.CreatedAt
            });
        }

        public async System.Threading.Tasks.Task MarkReportAsReadAsync(int reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);
            if (report != null)
            {
                report.IsRead = true;
                await _reportRepository.UpdateAsync(report);
            }
        }
    }
}
