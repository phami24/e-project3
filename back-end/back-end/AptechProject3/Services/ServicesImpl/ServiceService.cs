using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class ServiceService : IGenericService<Service>, IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Service> Create(Service entity)
        {
            try
            {
                await _unitOfWork.Services.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating service.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Service? deleteService = await _unitOfWork.Services.GetById(id);
                if (deleteService != null)
                {
                    await _unitOfWork.Services.Delete(deleteService);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting service.", ex);
            }
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            try
            {
                return await _unitOfWork.Services.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving services.", ex);
            }
        }

        public async Task<Service?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.Services.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving service by ID.", ex);
            }
        }

        public async Task<Service?> GetByName(string name)
        {
            try
            {
                return await _unitOfWork.Services.GetByName(name);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving service by ID.", ex);
            }
        }

        public async Task<Service> Update(Service entity)
        {
            try
            {
                await _unitOfWork.Services.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating service.", ex);
            }
        }
    }
}
