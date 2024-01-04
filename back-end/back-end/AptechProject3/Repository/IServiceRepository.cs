using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Repository
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<Service?> GetByName(string name);
    }
}
