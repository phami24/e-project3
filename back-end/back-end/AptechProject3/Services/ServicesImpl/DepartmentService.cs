using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> Create(Department entity)
        {
            try
            {
                await _unitOfWork.Departments.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating department.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Department? deleteDepartment = await _unitOfWork.Departments.GetById(id);
                if (deleteDepartment != null)
                {
                    await _unitOfWork.Departments.Delete(deleteDepartment);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting department.", ex);
            }
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            try
            {
                return await _unitOfWork.Departments.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving departments.", ex);
            }
        }

        public async Task<Department?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.Departments.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving department by ID.", ex);
            }
        }

        public async Task<Department> Update(Department entity)
        {
            try
            {
                await _unitOfWork.Departments.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating department.", ex);
            }
        }
    }
}
