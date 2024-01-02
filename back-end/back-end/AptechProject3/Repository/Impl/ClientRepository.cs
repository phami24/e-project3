using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<Client?> GetById(int id)
        {
            try
            {
                return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
