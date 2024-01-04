using AptechProject3.Models;

namespace AptechProject3.Services
{
    public interface IDepartmentService : IGenericService<Department>
    {
        Task<Department?> GetByName(string name);

        Task<bool> AddEmployee(List<Employee> employees, int departmentId);
    }
}
