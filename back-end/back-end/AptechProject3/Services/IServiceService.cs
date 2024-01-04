using AptechProject3.Models;

namespace AptechProject3.Services
{
    public interface IServiceService : IGenericService<Service>
    {
        Task<Service?> GetByName(string name);
    }
}
