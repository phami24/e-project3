using AptechProject3.Comon;
using AptechProject3.Repository;

namespace AptechProject3.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public IClientRepository Clients { get; set; }
        public IClientServiceRepository ClientServices { get; set; }
        public IDepartmentRepository Departments { get; set; }
        public IEmployeeRepository Employees { get; set; }
        public IPaymentRepository Payments { get; set; }
        public IReportRepository Reports { get; set; }
        public IServiceRepository Services { get; set; }
        public IServiceChargesRepository ServicesCharges { get; set; }


        public UnitOfWork(
            AppDbContext context,
            ILogger logger,
            IClientRepository clientRepository,
            IClientServiceRepository clientServiceRepository,
            IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository,
            IPaymentRepository paymentRepository,
            IReportRepository reportRepository,
            IServiceRepository serviceRepository,
            IServiceChargesRepository serviceChargesRepository)
        {
            _context = context;
            _logger = logger;
            Clients = clientRepository;
            ClientServices = clientServiceRepository;
            Departments = departmentRepository;
            Employees = employeeRepository;
            Payments = paymentRepository;
            Reports = reportRepository;
            Services = serviceRepository;
            ServicesCharges = serviceChargesRepository;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
