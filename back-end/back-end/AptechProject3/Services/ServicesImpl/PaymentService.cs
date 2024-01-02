using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class PaymentService : IGenericService<Payment>, IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Payment> Create(Payment entity)
        {
            try
            {
                await _unitOfWork.Payments.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating payment.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Payment? deletePayment = await _unitOfWork.Payments.GetById(id);
                if (deletePayment != null)
                {
                    await _unitOfWork.Payments.Delete(deletePayment);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting payment.", ex);
            }
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            try
            {
                return await _unitOfWork.Payments.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving payments.", ex);
            }
        }

        public async Task<Payment?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.Payments.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving payment by ID.", ex);
            }
        }

        public async Task<Payment> Update(Payment entity)
        {
            try
            {
                await _unitOfWork.Payments.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating payment.", ex);
            }
        }
    }
}
