using AptechProject3.Models;
using Microsoft.EntityFrameworkCore;

namespace AptechProject3.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<ClientService> ClientServices { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<ServiceCharges> ServiceCharges { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<Payment> Payments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
