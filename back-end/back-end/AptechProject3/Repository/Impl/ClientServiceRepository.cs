using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class ClientServiceRepository : GenericRepository<ClientService>, IClientServiceRepository
    {
        public ClientServiceRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<ClientService?> GetById(int id)
        {
            try
            {
                return await _context.ClientServices.AsNoTracking().FirstOrDefaultAsync(x => x.ClientServiceId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
