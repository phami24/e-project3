using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AptechProject3.Comon;
using AptechProject3.Models;

namespace AptechProject3.Services.ServicesImpl
{
    public class ClientServiceService : IGenericService<ClientService>, IClientServiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientService> Create(ClientService entity)
        {
            try
            {
                await _unitOfWork.ClientServices.Add(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while creating client service.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                ClientService? deleteClientService = await _unitOfWork.ClientServices.GetById(id);
                if (deleteClientService != null)
                {
                    await _unitOfWork.ClientServices.Delete(deleteClientService);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while deleting client service.", ex);
            }
        }

        public async Task<IEnumerable<ClientService>> GetAll()
        {
            try
            {
                return await _unitOfWork.ClientServices.All();
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving client services.", ex);
            }
        }

        public async Task<ClientService?> GetById(int id)
        {
            try
            {
                return await _unitOfWork.ClientServices.GetById(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while retrieving client service by ID.", ex);
            }
        }

        public async Task<ClientService> Update(ClientService entity)
        {
            try
            {
                await _unitOfWork.ClientServices.Update(entity);
                await _unitOfWork.CompleteAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error occurred while updating client service.", ex);
            }
        }
    }
}
