namespace AptechProject3.Services
{
    public interface IGenericService<T>
    {
        // Create a new entity
        Task<T> Create(T entity);

        // Read an entity by its unique identifier
        Task<T?> GetById(int id);

        // Update an existing entity
        Task<T> Update(T entity);

        // Delete an entity by its unique identifier
        Task Delete(int id);

        // Get all entities of type T
        Task<IEnumerable<T>> GetAll();
    }

}
