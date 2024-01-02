using AptechProject3.Comon;
using AptechProject3.Repository;
using AptechProject3.Repository.Impl;
using Microsoft.AspNetCore.Identity;

namespace AptechProject3.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly AuthDbContext _authDbContext;

        public IClientRepository Clients { get; set; }
        public IClientServiceRepository ClientServices { get; set; }
        public IDepartmentRepository Departments { get; set; }
        public IEmployeeRepository Employees { get; set; }
        public IPaymentRepository Payments { get; set; }
        public IReportRepository Reports { get; set; }
        public IServiceRepository Services { get; set; }
        public IServiceChargesRepository ServicesCharges { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }


        public UnitOfWork(
            AppDbContext context,
            AuthDbContext authDbContext,
            ILoggerFactory loggerFactory,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
           )
        {
            var _logger = loggerFactory.CreateLogger("logs");
            _context = context;
            _authDbContext = authDbContext;
            Clients = new ClientRepository(_context, _logger);
            ClientServices = new ClientServiceRepository(_context, _logger);
            Departments = new DepartmentRepository(_context, _logger);
            Employees = new EmployeeRepository(_context, _logger);
            Payments = new PaymentRepository(_context, _logger);
            Reports = new ReportRepository(_context, _logger);
            Services = new ServiceRepository(_context, _logger);
            ServicesCharges = new ServiceChargesRepository(_context, _logger);
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
            await _authDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
