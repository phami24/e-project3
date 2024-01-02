using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<Employee?> GetById(int id)
        {
            try
            {
                return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
