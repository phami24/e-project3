using Microsoft.AspNetCore.Identity.UI.Services;

namespace AptechProject3.Comon
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<bool> Add(T entity);
        Task<T?> GetById(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<int> Count();
        Task<IEnumerable<T>> GetAll(int page, int pageSize);

    }
}
