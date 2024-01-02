using AptechProject3.Comon;
using AptechProject3.Data;
using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Repository.Impl
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<Payment?> GetById(int id)
        {
            try
            {
                return await _context.Payments.AsNoTracking().FirstOrDefaultAsync(x => x.PaymentId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
