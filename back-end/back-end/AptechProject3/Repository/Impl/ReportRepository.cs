using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<Report?> GetById(int id)
        {
            try
            {
                return await _context.Reports.AsNoTracking().FirstOrDefaultAsync(x => x.ReportId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
