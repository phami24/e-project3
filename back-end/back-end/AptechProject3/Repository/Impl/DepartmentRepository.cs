using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<Department?> GetById(int id)
        {
            try
            {
                return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.DepartmentId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
