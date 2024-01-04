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

        public async Task<bool> AddEmployee(List<Employee> employees, int departmentId)
        {

            try
            {
                var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
                if (department == null)
                {
                    return false;
                }
                foreach (var employee in employees)
                {
                    department.Employees.Add(employee);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
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

        public async Task<Department?> GetByName(string name)
        {
            try
            {
                return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.DepartmentName == name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
