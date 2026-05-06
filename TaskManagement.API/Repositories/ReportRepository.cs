using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Entities;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Report>> GetByUserIdAsync(int userId)
        {
            return await _context.Reports
                .Include(r => r.Project)
                .Include(r => r.Sender)
                .Include(r => r.Receiver)
                .Where(r => r.SenderId == userId || r.ReceiverId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<Report?> GetByIdAsync(int reportId)
        {
            return await _context.Reports
                .Include(r => r.Project)
                .Include(r => r.Sender)
                .Include(r => r.Receiver)
                .FirstOrDefaultAsync(r => r.ReportId == reportId);
        }

        public async System.Threading.Tasks.Task UpdateAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }
    }
}
