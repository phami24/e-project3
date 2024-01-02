using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class ServiceChargesService : IServiceChargesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceChargesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceCharges> Create(ServiceCharges entity)
        {
            try
            {
                await _unitOfWork.ServicesCharges.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating service charges.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                ServiceCharges? deleteServiceCharges = await _unitOfWork.ServicesCharges.GetById(id);
                if (deleteServiceCharges != null)
                {
                    await _unitOfWork.ServicesCharges.Delete(deleteServiceCharges);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting service charges.", ex);
            }
        }

        public async Task<IEnumerable<ServiceCharges>> GetAll()
        {
            try
            {
                return await _unitOfWork.ServicesCharges.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving service charges.", ex);
            }
        }

        public async Task<ServiceCharges?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.ServicesCharges.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving service charges by ID.", ex);
            }
        }

        public async Task<ServiceCharges> Update(ServiceCharges entity)
        {
            try
            {
                await _unitOfWork.ServicesCharges.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating service charges.", ex);
            }
        }
    }
}
