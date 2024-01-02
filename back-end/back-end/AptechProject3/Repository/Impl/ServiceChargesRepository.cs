using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class ServiceChargesRepository : GenericRepository<ServiceCharges>, IServiceChargesRepository
    {
        public ServiceChargesRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<ServiceCharges?> GetById(int id)
        {
            try
            {
                return await _context.ServiceCharges.AsNoTracking().FirstOrDefaultAsync(x => x.ServiceChargesId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
