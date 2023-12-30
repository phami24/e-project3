using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;

namespace AptechProject3.Repository.Impl
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
