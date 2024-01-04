using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Repository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department?> GetByName(string name);
        Task<bool> AddEmployee(List<Employee> employees, int departmentId);
    }
}
