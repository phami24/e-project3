using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<Service?> GetById(int id)
        {
            try
            {
                return await _context.Services.AsNoTracking().FirstOrDefaultAsync(x => x.ServiceId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
