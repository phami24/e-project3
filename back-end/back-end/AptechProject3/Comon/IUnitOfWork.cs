using AptechProject3.Repository;

namespace AptechProject3.Comon
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IClientServiceRepository ClientServices { get; }
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        IPaymentRepository Payments { get; }
        IReportRepository Reports { get; }
        IServiceRepository Services { get; }
        IServiceChargesRepository ServicesCharges { get; }
        Task CompleteAsync();
    }
}
