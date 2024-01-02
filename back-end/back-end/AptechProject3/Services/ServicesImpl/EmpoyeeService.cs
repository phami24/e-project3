using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> Create(Employee entity)
        {
            try
            {
                await _unitOfWork.Employees.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating employee.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Employee? deleteEmployee = await _unitOfWork.Employees.GetById(id);
                if (deleteEmployee != null)
                {
                    await _unitOfWork.Employees.Delete(deleteEmployee);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting employee.", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                return await _unitOfWork.Employees.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving employees.", ex);
            }
        }

        public async Task<Employee?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.Employees.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving employee by ID.", ex);
            }
        }

        public async Task<Employee> Update(Employee entity)
        {
            try
            {
                await _unitOfWork.Employees.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating employee.", ex);
            }
        }
    }
}
