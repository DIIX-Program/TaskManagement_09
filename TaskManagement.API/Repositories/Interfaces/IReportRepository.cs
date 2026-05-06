using TaskManagement.API.Models.Entities;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface IReportRepository
    {
        System.Threading.Tasks.Task AddAsync(Report report);
        System.Threading.Tasks.Task<IEnumerable<Report>> GetByUserIdAsync(int userId);
        System.Threading.Tasks.Task<Report?> GetByIdAsync(int reportId);
        System.Threading.Tasks.Task UpdateAsync(Report report);
    }
}
